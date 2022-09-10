using LyricFinderCore.Finders.ChartLyrics;
using LyricFinderCore.Finders.LyricsOvh;
using LyricFinderCore.Models;

namespace LyricFinderConsoleTest
{
    [TestFixture]
    public class ChartLyricsTests
    {
        /// <summary>
        /// The unit test is testing the chart lyrics to search the lyric by valid artist name and song name.
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Boyzone", "Words")]
        public void GetLyric_InputValidSong_LyricCanBeFound(string artistName, string songName)
        {
            //arrange
            ChartLyricsFinder finder = new ChartLyricsFinder();
            Artist artist = new Artist(artistName);
            Song song = new Song(artist, songName);

            //act
            var result = finder.GetLyric(song);
            
            //assert
            Assert.That(result, Is.Not.Null);
        }

    }
}
