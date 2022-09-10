using LyricFinderConsole;
using LyricFinderCore.Models;

namespace LyricFinderConsoleTest
{
    [TestFixture]
    public class LyricFinderConsoleAppTests
    {
        /// <summary>
        /// The mock class for handle console readline.
        /// </summary>
        private class TestLyricFinderConsoleApp : LyricFinderConsoleApp
        {
            private readonly string _artistNumberInput;
            private readonly string _artistName;
            public TestLyricFinderConsoleApp(string artistName="", string artistNumberInput = "1")
            {
                _artistNumberInput = artistNumberInput;
                _artistName = artistName;
            }
            public override string? AskForArtistNumberInput()
            {
                return _artistNumberInput;
            }

            public override string? AskForInputArtistName()
            {
                return _artistName;
            }

            public override bool AskIfUserSearchAgain()
            {
                return false;
            }
        }

        /// <summary>
        /// Test the trysearchartist method with valid name input
        /// </summary>
        [Test]
        public void TrySearchArtist_InputValidName_ArtistCanBeFound()
        {
            //arrange
            LyricFinderConsoleApp app = new LyricFinderConsoleApp();
            //act
            var result = app.TrySearchArtist("Harry Styles");
            //assert
            Assert.That(result, Is.Not.Null);
        }
        /// <summary>
        /// Test the trysearchartist method with invalid name input
        /// </summary>
        [Test]
        public void TrySearchArtist_InputInvalidName_ArtistCanBeFound()
        {
            //arrange
            LyricFinderConsoleApp app = new LyricFinderConsoleApp();
            //act
            var result = app.TrySearchArtist("Harry xxxx");
            //assert
            Assert.That(result, Is.Null);
        }
        /// <summary>
        /// Test the TrySearchSimilarArtists method with valid name input
        /// </summary>
        [Test]
        public void TrySearchSimilarArtists_InputValidName_ArtistCanBeFound()
        {
            //arrange
            LyricFinderConsoleApp app = new TestLyricFinderConsoleApp();
            //act
            var result = app.TrySearchSimilarArtists("Harry");
            //assert
            Assert.That(result, Is.Not.Null);
        }
        /// <summary>
        /// Test the TrySearchSimilarArtists method with valid name input
        /// </summary>
        [Test]
        public void TrySearchSimilarArtists_InputInvalidName_ArtistCanBeFound()
        {
            //arrange
            LyricFinderConsoleApp app = new TestLyricFinderConsoleApp();
            //act
            var result = app.TrySearchSimilarArtists("Harry Stylesxxx");
            //assert
            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Test the TrySearchSongs method with valid artist
        /// </summary>
        [Test]
        public void TrySearchSongs_InputValidArtist_SongsCanBeFound()
        {
            //arrange
            Artist artist = new Artist("Harry Styles");
            LyricFinderConsoleApp app = new LyricFinderConsoleApp();
            //act
            var result = app.TrySearchSongs(artist);
            //assert
            Assert.That(result, Is.Not.Empty);
        }

        /// <summary>
        /// Test the TrySearchSongs method with invalid artist
        /// </summary>
        [Test]
        public void TrySearchSongs_InputInvalidArtist_SongsCanBeFound()
        {
            //arrange
            Artist artist = new Artist("Harry xxxxx");
            LyricFinderConsoleApp app = new LyricFinderConsoleApp();
            //act
            var result = app.TrySearchSongs(artist);
            //assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// Test the CalculateAverageWordCountOfLyrics method with valid songs
        /// </summary>
        [TestCase("Boyzone", "Words")]
        [TestCase("Michael Jackson", "Bad")]
        [TestCase("Lady Gaga", "Poker Face")]
        [TestCase("Westlife", "I Need You")]
        public void CalculateAverageWordCountOfLyrics_InputValidSongs_AverageCanBeFound(string artistName, string songTitle)
        {
            //arrange
            Artist artist = new Artist(artistName);
            Song song = new Song(artist, songTitle);
            List<Song> songs = new List<Song>(){song}; 
            LyricFinderConsoleApp app = new LyricFinderConsoleApp();
            //act
            var result = app.CalculateAverageWordCountOfLyrics(songs);
            //assert
            Assert.That(result, Is.GreaterThan(0));
        }

        /// <summary>
        /// Test Run
        /// </summary>
        [TestCase("Boyzone", "")]
        [TestCase("Michael", "10")]
        public void Run_AllSteps_Success(string artistName, string artistNumberInput)
        {
            //arrange
            LyricFinderConsoleApp app = new TestLyricFinderConsoleApp(artistName, artistNumberInput);
            //act
            app.Run();
            //assert
            Assert.Pass();
        }
    }
}
