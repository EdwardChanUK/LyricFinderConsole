using LyricFinderCore.Finders.LyricsOvh;
using LyricFinderCore.Models;

namespace LyricFinderConsoleTest
{
    [TestFixture]
    public class LyricsOvhTests
    {
        /// <summary>
        /// The unit test is testing the lyrics ovh to search the lyric by valid artist name and song name.
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Harry Styles", "As it was")]
        public void GetLyric_InputValidSong_LyricCanBeFound(string artistName, string songName)
        {
            //arrange
            LyricsOvhFinder finder = new LyricsOvhFinder();
            Artist artist = new Artist(artistName);
            Song song = new Song(artist, songName);

            //act
            var result = finder.GetLyric(song);
            
            //assert
            Assert.That(result, Is.Not.Null);
        }

        /// <summary>
        /// The unit test is testing the lyrics ovh to search the lyric by invalid artist name and song name.
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Harry Styles", "Sunflower, Vol. 6")]
        public void GetLyric_InputInvalidSong_LyricCannotBeFound(string artistName, string songName)
        {
            //arrange
            LyricsOvhFinder finder = new LyricsOvhFinder();
            Artist artist = new Artist(artistName);
            Song song = new Song(artist, songName);

            //act
            var result = finder.GetLyric(song);

            //assert
            Assert.That(result, Is.Null);
        }

    }
}
