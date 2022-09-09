using LyricFinderCore.Models;

namespace LyricFinderCore.Interfaces
{
    /// <summary>
    /// This interface describes the methods to search the songs
    /// </summary>
    public interface ISongFinder
    {
        /// <summary>
        /// This method is used to search the songs of the artist.
        /// </summary>
        /// <param name="artist"></param>
        /// <returns></returns>
        public List<Song> SearchSongs(Artist artist);
    }
}
