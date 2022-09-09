using System.Net;
using System.Net.Http.Json;
using LyricFinderCore.Comparer;
using LyricFinderCore.Interfaces;
using LyricFinderCore.Models;

namespace LyricFinderCore.Finders.MusicBrainz
{
    public class MusicBrainzFinder : IArtistFinder, ISongFinder
    {
        public const string UserAgent = "LyricFinder/1.0";
        public const int MaxRetryTime = 5;
        public const int WaitingTimeWhenRetry = 3000;
        public const int RecordLimit = 100;
        public const string MusicBrainzBaseUrl = "http://musicbrainz.org/ws/2";

        public Artist? SearchArtist(string name)
        {
            var result = Task.Run(() => RunSearchArtistAsync(name)).Result;
            return result?.Artists.FirstOrDefault(a => a.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }
        public List<Artist> SearchArtists(string partialName)
        {
            var result = Task.Run(() => RunSearchArtistAsync(partialName)).Result;
            var artists = result?.Artists.Distinct(ArtistComparer.Instance).ToList();
            return artists??new List<Artist>();
        }

        public List<Song> SearchSongs(Artist artist)
        {
            var musicBrainzArtist = SearchArtist(artist.Name) as MusicBrainzArtist;
            if (musicBrainzArtist == null)
                return new List<Song>();
            return SearchSongs(musicBrainzArtist);
        }
        public List<Song> SearchSongs(MusicBrainzArtist artist)
        {
            HashSet<Song> songs = new HashSet<Song>();
            List<Song>? newSongs;
            var songsOffset = 0;
            do
            {
                var result = Task.Run(() => RunSearchSongAsync(artist.Id, songsOffset)).Result;
                newSongs = result?.Recordings.Distinct(SongComparer.Instance).Select(a => new Song(artist, a.Title)).ToList();
                if(newSongs==null)
                    break;
                songs.UnionWith(newSongs);
                songsOffset += RecordLimit;
            } while (newSongs?.Count > 0);
            return songs.ToList();
        }
        private async Task<MusicBrainzSearchResult?> RunSearchArtistAsync(string artistName)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            string path = $"{MusicBrainzBaseUrl}/artist/?fmt=json&query=artist:%22{artistName}%22";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MusicBrainzSearchResult>().ConfigureAwait(false);
            }
            return null;
        }

        private async Task<MusicBrainzRecordingList?> RunSearchSongAsync(string artistId, int offset=0)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            string path = $"{MusicBrainzBaseUrl}/recording?limit={RecordLimit}&fmt=json&artist={artistId}&offset={offset}";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<MusicBrainzRecordingList>().ConfigureAwait(false);
            }

            var retryTime = 0;
            while (response.StatusCode == HttpStatusCode.ServiceUnavailable && retryTime<MaxRetryTime)
            {
                //retry
                response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<MusicBrainzRecordingList>().ConfigureAwait(false);
                }
                Thread.Sleep(WaitingTimeWhenRetry);
                retryTime++;
            }
            return null;
        }
    }
}
