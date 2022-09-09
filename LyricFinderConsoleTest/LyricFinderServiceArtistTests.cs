using LyricFinderCore;
using LyricFinderCore.Exceptions;
using LyricFinderCore.Interfaces;
using LyricFinderCore.Models;

namespace LyricFinderConsoleTest
{
    [TestFixture]
    public class LyricFinderServiceArtistTests
    {
        /// <summary>
        /// Mock class of ArtistFinder to test the lyric finder service
        /// </summary>
        private class MockArtistFinder : IArtistFinder
        {
            private readonly List<Artist> _mockArtists;
            public MockArtistFinder(List<Artist> mockArtists)
            {
                _mockArtists = mockArtists;
            }
            public Artist? SearchArtist(string name)
            {
                foreach (var mockArtist in _mockArtists)
                {
                    if (mockArtist.Name.Equals(name))
                        return mockArtist;
                }
                return null;
            }
            public List<Artist> SearchArtists(string partialName)
            {
                List<Artist> result = new List<Artist>();
                foreach (var mockArtist in _mockArtists)
                {
                    if (mockArtist.Name.Contains(partialName))
                    {
                        result.Add(mockArtist);
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// This unit test is testing if the lyric finder has no artist finder and search artist
        /// </summary>
        [Test]
        public void SearchArtist_WhenNoArtistFinder_ArtistCannotBeFound()
        {
            //arrange
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>(),
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            var result = service.SearchArtist("test");

            //assert
            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service can search artist when input valid name
        /// </summary>
        [Test]
        public void SearchArtist_WhenInputValidName_ArtistCanBeFound()
        {
            //arrange
            Artist mockArtist = new Artist("test");
            List<Artist> mockArtists = new List<Artist>() {mockArtist};
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>() { new MockArtistFinder(mockArtists) },
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            var result = service.SearchArtist("test");

            //assert
            Assert.That(result, Is.EqualTo(mockArtist));
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service can search artist when input invalid name
        /// </summary>
        [Test]
        public void SearchArtist_WhenInputInvalidName_ArtistCannotBeFound()
        {
            //arrange
            List<Artist> mockArtists = new List<Artist>() { new Artist("test") };
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>() { new MockArtistFinder(mockArtists) },
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            var result = service.SearchArtist("invalid");

            //assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void SearchArtists_WhenNoArtistFinder_ArtistsCannotBeFound()
        {
            //arrange
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>(),
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            var result = service.SearchArtists("test");

            //assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service can search artists when input valid partial name
        /// </summary>
        [TestCase("est", 2)]
        [TestCase("tes", 1)]
        public void SearchArtists_WhenInputValidPartialName_ArtistsCanBeFound(
            string partialName, int expectedCount)
        {
            //arrange
            List<Artist> mockArtists = new List<Artist>()
            {
                new Artist("test"), 
                new Artist("rest")
            };
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>() { new MockArtistFinder(mockArtists) },
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            var result = service.SearchArtists(partialName);

            //assert
            Assert.That(result.Count, Is.EqualTo(expectedCount));
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service cannot search artists when input invalid partial name
        /// </summary>
        [Test]
        public void SearchArtists_WhenInputInvalidPartialName_ArtistsCannotBeFound()
        {
            //arrange
            List<Artist> mockArtists = new List<Artist>()
            {
                new Artist("test"),
                new Artist("rest")
            };
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>() { new MockArtistFinder(mockArtists) },
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            var result = service.SearchArtists("invalid");

            //assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// This unit test is testing if the lyric finder service will throw invalid partial name exception when search artists and input empty partial name
        /// </summary>
        [TestCase(null)]
        [TestCase("")]
        public void SearchArtists_WhenInputEmptyName_ThrowsInvalidPartialNameException(string partialName)
        {
            //arrange
            List<Artist> mockArtists = new List<Artist>()
            {
                new Artist("test"),
                new Artist("rest")
            };
            LyricFinderService service = new LyricFinderService(
                new List<IArtistFinder>() { new MockArtistFinder(mockArtists) },
                new List<ISongFinder>(),
                new List<ILyricFinder>());

            //act
            //assert
            Assert.That(() => service.SearchArtists(partialName), Throws.InstanceOf<InvalidPartialNameException>());
        }
        
    }
}