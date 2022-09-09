﻿using LyricFinderCore.Finders.MusicBrainz;
using LyricFinderCore.Models;

namespace LyricFinderConsoleTest
{
    [TestFixture]
    public class MusicBrainzTests
    {
        /// <summary>
        /// The unit test is testing the music brainz to search the artist by valid name
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Michael Jackson")]
        [TestCase("David Bowie")]
        public async Task SearchArtist_InputValidName_ArtistCanBeFound(string artistName)
        {
            //arrange
            MusicBrainzFinder finder = new MusicBrainzFinder();
            //act
            var result = finder.SearchArtist(artistName);
            //assert
            Assert.That(result?.Name, Is.EqualTo(artistName));
        }

        /// <summary>
        /// The unit test is testing the music brainz to search the artist by invalid name
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Michael xxx")]
        [TestCase("David xxx")]
        public async Task SearchArtist_InputInvalidName_ArtistCanBeFound(string artistName)
        {
            //arrange
            MusicBrainzFinder finder = new MusicBrainzFinder();
            //act
            var result = finder.SearchArtist(artistName);
            //assert
            Assert.That(result?.Name, Is.Null);
        }

        /// <summary>
        /// The unit test is testing the music brainz to search the artists by valid name
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Michael")]
        [TestCase("David")]
        public async Task SearchArtists_InputValidPartialName_ArtistCanBeFound(string partialName)
        {
            //arrange
            MusicBrainzFinder finder = new MusicBrainzFinder();
            //act
            var result = finder.SearchArtists(partialName);
            //assert
            Assert.That(result, Is.Not.Empty);
        }

        /// <summary>
        /// The unit test is testing the music brainz to search the artists by invalid name
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Michaelxxx")]
        [TestCase("Davidxxx")]
        public async Task SearchArtists_InputInvalidPartialName_ArtistCanBeFound(string partialName)
        {
            //arrange
            MusicBrainzFinder finder = new MusicBrainzFinder();
            //act
            var result = finder.SearchArtists(partialName);
            //assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// The unit test is testing the music brainz to search songs with valid artist
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Michael Jackson")]
        [TestCase("David Bowie")]
        [TestCase("Harry Styles")]
        [TestCase("Westlife")]
        public async Task SearchSongs_InputValidArtist_SongsCanBeFound(string artistName)
        {
            //arrange
            MusicBrainzFinder finder = new MusicBrainzFinder();
            //act
            var result = finder.SearchSongs(new Artist(artistName));
            //assert
            Assert.That(result, Is.Not.Empty);
        }

        /// <summary>
        /// The unit test is testing the music brainz to search songs with invalid artist
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("Michael xxxx")]
        [TestCase("David xxxx")]
        public async Task SearchSongs_InputInvalidArtist_SongsCanBeFound(string artistName)
        {
            //arrange
            MusicBrainzFinder finder = new MusicBrainzFinder();
            //act
            var result = finder.SearchSongs(new Artist(artistName));
            //assert
            Assert.That(result, Is.Empty);
        }

        /// <summary>
        /// The unit test is testing the music brainz to search songs with valid artist id.
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [TestCase("65f4f0c5-ef9e-490c-aee3-909e7ae6b2ab")]
        public async Task SearchSongs_InputValidArtistId_SongsCanBeFound(string artistId)
        {
            //arrange
            MusicBrainzFinder finder = new MusicBrainzFinder();
            MusicBrainzArtist artist = new MusicBrainzArtist(artistId);
            artist.Id = artistId;
            //act
            var result = finder.SearchSongs(artist);
            //assert
            Assert.That(result, Is.Not.Empty);
        }

        /// <summary>
        /// The unit test is testing the music brainz to search songs with invalid artist id
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        [TestCase("65f4f0c5-ef9e-490c-aee3-909e7ae6b2abxxx")]
        public async Task SearchSongs_InputInvalidArtistId_SongsCanBeFound(string artistId)
        {
            //arrange
            MusicBrainzFinder finder = new MusicBrainzFinder();
            MusicBrainzArtist artist = new MusicBrainzArtist(artistId);
            artist.Id = artistId;
            //act
            var result = finder.SearchSongs(artist);
            //assert
            Assert.That(result, Is.Empty);
        }

    }
}
