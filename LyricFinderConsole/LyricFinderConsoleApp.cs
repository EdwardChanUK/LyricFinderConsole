using LyricFinderCore;
using LyricFinderCore.Finders.LyricsOvh;
using LyricFinderCore.Finders.MusicBrainz;
using LyricFinderCore.Helpers;
using LyricFinderCore.Interfaces;
using LyricFinderCore.Models;

namespace LyricFinderConsole
{
    public class LyricFinderConsoleApp
    {
        private const int MaxInputArtistNumberTime = 3;
        private readonly LyricFinderService _service;

        /// <summary>
        /// The constructor method of Lyric finder console app.
        /// </summary>
        public LyricFinderConsoleApp()
        {
            MusicBrainzFinder musicBrainzFinder = new MusicBrainzFinder();
            LyricsOvhFinder lyricsOvhFinder = new LyricsOvhFinder();
            _service = new LyricFinderService(
                new List<IArtistFinder>()
                {
                    musicBrainzFinder
                },
                new List<ISongFinder>()
                {
                    musicBrainzFinder
                },
                new List<ILyricFinder>()
                {
                    lyricsOvhFinder
                });
        }
        /// <summary>
        /// Run the console app
        /// </summary>
        public void Run()
        {
            Start();
            do
            {
                Artist? artist = null;
                while (artist == null)
                {
                    var artistNameInput = AskForInputArtistName();
                    artist = TrySearchArtist(artistNameInput);
                    artist ??= TrySearchSimilarArtists(artistNameInput);
                }

                var songs = TrySearchSongs(artist);
                CalculateAverageWordCountOfLyrics(songs);
            } while (AskIfUserSearchAgain());
        }

        /// <summary>
        /// Ask if the user want to search again.
        /// </summary>
        /// <returns></returns>
        public virtual bool AskIfUserSearchAgain()
        {
            while (true)
            {
                Console.WriteLine("Do you want to search again(Y/N)?");
                var result = Console.ReadLine();
                if ("Y".Equals(result))
                    return true;
                if ("Y".Equals(result))
                    return false;
                Console.WriteLine("Your input is incorrect. Please input 'Y' or 'N'.");
            }
        }

        /// <summary>
        /// Calculate the average word count of the lyrics
        /// </summary>
        /// <param name="songs"></param>
        public float CalculateAverageWordCountOfLyrics(List<Song> songs)
        {
            Console.WriteLine("I am searching the lyrics of songs. Please wait...");
            var totalLyrics = 0;

            var totalWordsInLyrics = 0;
            foreach (var song in songs)
            {
                var lyric = _service.GetLyric(song);
                if (lyric != null)
                {
                    totalLyrics++;
                    totalWordsInLyrics += WordCounter.WordsCount(lyric.Content);
                }
            }

            float averageWordsCount = totalWordsInLyrics / (float)totalLyrics;
            Console.WriteLine($"The average word count of the lyrics is {averageWordsCount}.");
            return averageWordsCount;
        }

        /// <summary>
        /// Try to search the songs
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        public List<Song> TrySearchSongs(Artist artist)
        {
            Console.WriteLine($"I am searching the songs of '{artist.Name}'. Please wait...");
            var songs = _service.SearchSongs(artist);
            Console.WriteLine("The artist has " + songs.Count + " songs.");
            return songs;
        }

        /// <summary>
        /// Start the console app with welcome text
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Welcome to Lyrics Finder");
        }
        /// <summary>
        /// Ask the user to input artist name
        /// </summary>
        /// <returns></returns>
        public virtual string? AskForInputArtistName()
        {
            Console.WriteLine("Would you input the artist's name?");
            return Console.ReadLine();
        }
        /// <summary>
        /// Try to search the artist
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        public Artist? TrySearchArtist(string? artistName)
        {
            if (String.IsNullOrEmpty(artistName))
            {
                Console.WriteLine("The artist name cannot be empty.");
                return null;
            }
            var artist = _service.SearchArtist(artistName);
            if (artist != null)
                return artist;
            Console.WriteLine($"I cannot found the artist who named '{artistName}'.");
            return null;
        }
        /// <summary>
        /// Ask the user input artist number.
        /// </summary>
        /// <returns></returns>
        public virtual string? AskForArtistNumberInput()
        {
            Console.WriteLine("Please input the number which artist you want to select?");
            return Console.ReadLine();
        }
        /// <summary>
        /// Try to search the similar artists.
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        public Artist? TrySearchSimilarArtists(string? artistName)
        {
            if (String.IsNullOrEmpty(artistName))
                return null;

            Console.WriteLine("I am searching the similar artists. Please wait...");
            var similarArtists = _service.SearchArtists(artistName);
            if (similarArtists?.Count == 0)
            {
                Console.WriteLine("There is no artists has similar name.");
                return null;
            }

            Console.WriteLine($"I found the following artists has similar name:");
            var index = 1;
            foreach (var similarArtist in similarArtists)
            {
                Console.WriteLine($"{index++}. {similarArtist.Name}");
            }

            var artistNumberStr = AskForArtistNumberInput();
            var artistNumber = 0;

            var isFound = false;
            var inputTime = 0;
            while (inputTime < MaxInputArtistNumberTime)
            {
                var canParse = Int32.TryParse(artistNumberStr, out artistNumber);
                if (canParse && artistNumber < similarArtists.Count)
                {
                    isFound = true;
                    break;
                }

                inputTime++;
                Console.WriteLine("Your input is not correct, please input again.");
                artistNumberStr = Console.ReadLine();
            }

            if (isFound)
                return similarArtists[artistNumber - 1];
            return null;
        }
    }
}
