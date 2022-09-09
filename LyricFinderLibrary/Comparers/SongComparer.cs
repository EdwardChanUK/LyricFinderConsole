using LyricFinderCore.Models;

namespace LyricFinderCore.Comparer
{
    public class SongComparer: IEqualityComparer<Song>
    {
        public static readonly SongComparer Instance = new SongComparer();
        private SongComparer(){}
        public bool Equals(Song? x, Song? y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y != null)
                return x.Equals(y);
            return false;
        }

        public int GetHashCode(Song obj)
        {
            if (obj.Artist == null)
                return obj.Title.GetHashCode();
            return HashCode.Combine(obj.Artist.GetHashCode(), obj.Title.GetHashCode());
        }
    }
    
}
