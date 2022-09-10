using LyricFinderCore.Models;

namespace LyricFinderCore.Finders.MusicBrainz
{
    public class MusicBrainzArtist : Artist
    {
        public string? Id { get; set; }
        public MusicBrainzArtist(string name) : base(name)
        {
        }
    }
}
