using LyricFinderCore.Models;

namespace LyricFinderCore.Interfaces
{
    /// <summary>
    /// This interface describes the methods which finding the artist
    /// </summary>
    public interface IArtistFinder
    {
        /// <summary>
        /// This method is used to search the artist name which exact match.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Artist? SearchArtist(string name);
        /// <summary>
        /// This method is used to search the artist name which partial match.
        /// </summary>
        /// <param name="partialName"></param>
        /// <returns></returns>
        public List<Artist> SearchArtists(string partialName);
    }
}
