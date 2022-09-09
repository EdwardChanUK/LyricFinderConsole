namespace LyricFinderCore.Models
{
    /// <summary>
    /// This the model of the song.
    /// </summary>
    public class Song : IEquatable<Song>
    {
        public Artist Artist { get; set; }
        public string Title { get; set; }
        public Song(Artist artist, string title)
        {
            Artist = artist;
            Title = title;
        }

        public bool Equals(Song? obj)
        {
            if(obj == null) return false;
            if (!Artist.Equals(obj.Artist))
                return false;
            if (String.IsNullOrEmpty(Title) || String.IsNullOrEmpty(obj.Title))
                return false;
            return Title.Equals(obj.Title);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return Equals(obj as Song);
        }
    }
}
