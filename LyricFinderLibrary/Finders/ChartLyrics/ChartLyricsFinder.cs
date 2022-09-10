using LyricFinderCore.Finders.LyricsOvh;
using LyricFinderCore.Interfaces;
using LyricFinderCore.Models;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Serialization;

namespace LyricFinderCore.Finders.ChartLyrics
{
    /// <summary>
    /// This is the class using ChartLyrics api to search lyrics
    /// </summary>
    public class ChartLyricsFinder : ILyricFinder
    {
        private const string ChartLyricsBaseUrl = "http://api.chartlyrics.com/apiv1.asmx";
        public Lyric? GetLyric(Song song)
        {
            var result = Task.Run(() => RunSearchLyricAsync(song.Artist.Name, song.Title)).Result;
            var lyricsContent = result?.Lyric;
            if (String.IsNullOrEmpty(lyricsContent))
                return null;
            return new Lyric(song, lyricsContent);
        }
        private async Task<ChartLyricsSearchResult?> RunSearchLyricAsync(string artistName, string songName)
        {
            HttpClient client = new HttpClient();
            string path = $"{ChartLyricsBaseUrl}/SearchLyricDirect?artist={artistName}&song={songName}";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var contentStr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var buffer = Encoding.UTF8.GetBytes(contentStr);
                using var stream = new MemoryStream(buffer);
                var serializer = new XmlSerializer(typeof(ChartLyricsSearchResult));
                return serializer.Deserialize(stream) as ChartLyricsSearchResult;
            }
            return null;
        }
    }
}
