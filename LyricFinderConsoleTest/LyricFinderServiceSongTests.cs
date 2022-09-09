using LyricFinderCore;
using LyricFinderCore.Exceptions;
using LyricFinderCore.Interfaces;
using LyricFinderCore.Models;

namespace LyricFinderConsoleTest
{
    [TestFixture]
    public class LyricFinderServiceSongTests
    {
        /// <summary>
        /// Mock class of SongFinder to test the lyric finder service
        /// </summary>
        private class MockSongFinder : ISongFinder
        {
            private readonly List<Song> _mockSongs;
            public MockSongFinder(List<Song> mockSongs)
            {
                _mockSongs = mockSongs;
            }
            public List<Song> SearchSongs(Artist artist)
            {
                List<Song> result = new List<Song>();
                foreach (var mockSong in _mockSongs)
                {
                    if (mockSong.Artist.Equals(artist))
                        result.Add(mockSong);
                }

                return result;
            }
        }

        /// <summary>
        /// This unit test is testing if the lyric finder has no song finder and search song
        /// </summary>
        [Test]
        public void SearchArtist_WhenNoSongFinder_SongsCannotBeFound()
        {
            //arrange
            Artist mockArtist = new Artist("test");
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>(),
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            var result = service.SearchSongs(mockArtist);

            //assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service can search song when input valid artist
        /// </summary>
        [TestCase("test", 2)]
        [TestCase("test2", 1)]
        public void SearchArtist_WhenInputValidArtist_SongsCanBeFound(string artistName, int expectedCount)
        {
            //arrange
            Artist mockArtist = new Artist("test");
            Artist mockArtist2 = new Artist("test2");
            List<Song> mockSongs = new List<Song>()
            {
                new Song(mockArtist, "song1"),
                new Song(mockArtist, "song2"),
                new Song(mockArtist2, "song3"),
            };
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>(),
                new List<ISongFinder>(){new MockSongFinder(mockSongs)},
                new List<ILyricFinder>());

            //act
            var result = service.SearchSongs(new Artist(artistName));

            //assert
            Assert.That(result.Count, Is.EqualTo(expectedCount));
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service cannot search song when input invalid artist.
        /// </summary>
        [Test]
        public void SearchArtist_WhenInputInvalidArtist_SongsCannotBeFound()
        {
            //arrange
            Artist mockArtist = new Artist("test");
            Artist mockArtist2 = new Artist("test2");
            List<Song> mockSongs = new List<Song>()
            {
                new Song(mockArtist, "song1"),
                new Song(mockArtist, "song2"),
                new Song(mockArtist2, "song3"),
            };
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>(),
                new List<ISongFinder>(){new MockSongFinder(mockSongs)},
                new List<ILyricFinder>());

            //act
            var result = service.SearchSongs(new Artist("invalid"));

            //assert
            Assert.That(result, Is.Empty);
        }
    }
}