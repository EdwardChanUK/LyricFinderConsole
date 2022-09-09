using LyricFinderCore.Interfaces;
using LyricFinderCore.Models;
using System.Net.Http.Json;

namespace LyricFinderCore.Finders.LyricsOvh
{
    /// <summary>
    /// This is the class using LyricsOvh api for finding lyrics
    /// </summary>
    public class LyricsOvhFinder:ILyricFinder
    {
        private const string LyricsOvhBaseUrl = "https://private-5b143-lyricsovh.apiary-mock.com/v1/";
        public Lyric? GetLyric(Song song)
        {
            var result = Task.Run(() => RunSearchLyricAsync(song.Artist.Name, song.Title)).Result;
            var lyricsContent = result?.Lyrics;
            if (String.IsNullOrEmpty(lyricsContent))
                return null;
            return new Lyric(song, lyricsContent);
        }

        private async Task<LyricsOvhResult?> RunSearchLyricAsync(string artistName, string songName)
        {
            HttpClient client = new HttpClient();
            string path = $"{LyricsOvhBaseUrl}/{artistName}/{songName}";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LyricsOvhResult>().ConfigureAwait(false);
            }
            return null;
        }

    }
}
