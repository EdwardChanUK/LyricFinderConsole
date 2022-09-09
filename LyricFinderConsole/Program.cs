// See https://aka.ms/new-console-template for more information

using LyricFinderCore;
using LyricFinderCore.Finders.LyricsOvh;
using LyricFinderCore.Finders.MusicBrainz;
using LyricFinderCore.Helpers;
using LyricFinderCore.Interfaces;

const int MaxInputArtistNumberTime = 3;
MusicBrainzFinder musicBrainzFinder = new MusicBrainzFinder();
LyricsOvhFinder lyricsOvhFinder = new LyricsOvhFinder();
LyricFinderService service = new LyricFinderService(
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

Console.WriteLine("Welcome to Lyrics Finder");
Console.WriteLine("Would you input the artist's name?");
var searchWord = Console.ReadLine();
var artist = service.SearchArtist(searchWord);

if (artist == null)
{
    var similarArtists = service.SearchArtists(searchWord);
    Console.WriteLine($"I am sorry that I cannot found the artist who named '{searchWord}'. But I found the following artist has similar name:");
    int index = 1;
    foreach (var similarArtist in similarArtists)
    {
        Console.WriteLine($"{index++}. {similarArtist.Name}");
    }
    Console.WriteLine("Please input the number which artist you want to select?");
    var artistNumberStr = Console.ReadLine();
    var artistNumber = 0;

    var isFound = false;
    var inputTime = 0;
    while (inputTime < MaxInputArtistNumberTime)
    {
        var canParse=Int32.TryParse(artistNumberStr, out artistNumber);
        if (canParse && artistNumber < similarArtists.Count)
        {
            isFound = true;
            break;
        }
        inputTime++;
        Console.WriteLine("Your input is not correct, please input again.");
        artistNumberStr = Console.ReadLine();
    }
    if(isFound)
        artist = similarArtists[artistNumber - 1];
}

Console.WriteLine("I am searching the songs of '"+artist.Name+"'. Please wait...");
var songs = service.SearchSongs(artist);
Console.WriteLine("The artist has " + songs.Count + " songs.");

Console.WriteLine("I am searching the lyrics of songs. Please wait...");
var totalLyrics = 0;
var totalWordsInLyrics = 0;
foreach (var song in songs)
{
    var lyric = service.GetLyric(song);
    if (lyric != null)
    {
        totalLyrics++;
        totalWordsInLyrics += WordCounter.WordsCount(lyric.Content);
    }
}

float averageWordsCount = totalWordsInLyrics / (float)totalLyrics;
Console.WriteLine($"The average word count of the lyrics is {averageWordsCount}.");

Console.ReadLine();