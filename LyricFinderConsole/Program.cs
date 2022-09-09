// See https://aka.ms/new-console-template for more information

using LyricFinderCore;
using LyricFinderCore.Finders.MusicBrainz;
using LyricFinderCore.Interfaces;

MusicBrainzFinder musicBrainzFinder = new MusicBrainzFinder();
LyricFinderService service = new LyricFinderService(
    new List<IArtistFinder>()
    {
        musicBrainzFinder
    },
    new List<ISongFinder>()
    {
        musicBrainzFinder
    },
    new List<ILyricFinder>());
Console.WriteLine("Welcome to Lyrics Finder");
Console.WriteLine("Would you input the artist's name?");
var searchWord = Console.ReadLine();
var artist = service.SearchArtist(searchWord);
if (artist == null)
{
    var similarArtists = service.SearchArtists(searchWord);
    Console.WriteLine("I am sorry that I cannot found the artist who named '" + searchWord + "'. But I found the following artist has similar name:");
    int index = 1;
    foreach (var similarArtist in similarArtists)
    {
        Console.WriteLine((index++) + ". " + similarArtist.Name);
    }
    Console.WriteLine("Please input the number which you want?");
    var artistNumberStr = Console.ReadLine();
    var artistNumber = 0;

    var isFound = false;
    var maxInputTime = 3;
    var inputTime = 0;
    while (inputTime<maxInputTime)
    {
        var canParse=Int32.TryParse(artistNumberStr, out artistNumber);
        if (canParse && artistNumber < similarArtists.Count)
        {
            isFound = true;
            break;
        }
        else
        {
            inputTime++;
            Console.WriteLine("Your input is not correct, please input again.");
            artistNumberStr = Console.ReadLine();
        }
    }
    artist = similarArtists[artistNumber - 1];
}

Console.WriteLine("I am searching the songs of '"+artist.Name+"'. Please wait...");
var songs = service.SearchSongs(artist);
Console.WriteLine("The artist has " + songs.Count + " songs.");
Console.ReadLine();