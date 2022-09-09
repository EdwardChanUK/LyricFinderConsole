using LyricFinderCore;
using LyricFinderCore.Interfaces;
using LyricFinderCore.Models;

namespace LyricFinderConsoleTest
{
    [TestFixture]
    public class LyricFinderServiceLyricTests
    {
        /// <summary>
        /// Mock class of LyricFinder to test the lyric finder service
        /// </summary>
        private class MockLyricFinder : ILyricFinder
        {
            private readonly List<Lyric> _mockLyrics;
            public MockLyricFinder(List<Lyric> mockLyrics)
            {
                _mockLyrics = mockLyrics;
            }

            public Lyric? GetLyric(Song song)
            {
                foreach (var mockLyric in _mockLyrics)
                {
                    if(mockLyric.Song.Equals(song))
                        return mockLyric;
                }
                return null;
            }
        }

        /// <summary>
        /// This unit test is testing if the lyric finder has no lyric finder and search lyric
        /// </summary>
        [Test]
        public void GetLyric_WhenNoLyricFinder_LyricsCannotBeFound()
        {
            //arrange
            Artist mockArtist = new Artist("test");
            Song mockSong = new Song(mockArtist, "song1");
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>(),
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            var result = service.GetLyric(mockSong);

            //assert
            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service can find lyric when input valid song
        /// </summary>
        [Test]
        public void GetLyric_WhenInputValidSong_LyricCanBeFound()
        {
            //arrange
            Artist mockArtist = new Artist("test");
            Song mockSong = new Song(mockArtist, "song1");
            Lyric mockLyric = new Lyric(mockSong, "This is the lyrics.");
            List<Lyric> mockLyrics = new List<Lyric>() { mockLyric };
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>(),
                new List<ISongFinder>(),
                new List<ILyricFinder>(){new MockLyricFinder(mockLyrics)});

            //act
            var result = service.GetLyric(new Song(mockArtist, "song1"));

            //assert
            Assert.That(result, Is.EqualTo(mockLyric));
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service cannot find lyric when input invalid song.
        /// </summary>
        [TestCase("test", "song2")]
        [TestCase("test2", "song1")]
        [TestCase("test2", "song2")]
        public void GetLyric_WhenInputInvalidSong_LyricCannotBeFound(string artistName, string songTitle)
        {
            //arrange
            Artist mockArtist = new Artist("test");
            Song mockSong = new Song(mockArtist, "song1");
            Lyric mockLyric = new Lyric(mockSong, "This is the lyrics.");
            List<Lyric> mockLyrics = new List<Lyric>() { mockLyric };
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>(),
                new List<ISongFinder>(),
                new List<ILyricFinder>() { new MockLyricFinder(mockLyrics) });

            //act
            var result = service.GetLyric(new Song(new Artist(artistName), songTitle));

            //assert
            Assert.That(result, Is.Null);
        }
    }
}