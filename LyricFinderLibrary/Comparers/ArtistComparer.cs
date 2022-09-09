using LyricFinderCore.Models;

namespace LyricFinderCore.Comparer
{
    public class ArtistComparer: IEqualityComparer<Artist>
    {
        public static readonly ArtistComparer Instance = new ArtistComparer();
        private ArtistComparer(){}
        public bool Equals(Artist? x, Artist? y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y != null)
                return x.Equals(y);
            return false;
        }

        public int GetHashCode(Artist obj)
        {
            return obj.Name.GetHashCode();
        }
    }
    
}
