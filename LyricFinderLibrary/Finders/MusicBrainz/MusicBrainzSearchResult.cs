namespace LyricFinderCore.Finders.MusicBrainz
{
    public class MusicBrainzSearchResult
    {
        public int Count { get; set; }
        public MusicBrainzArtist[] Artists { get; set; } = Array.Empty<MusicBrainzArtist>();
    }
}
