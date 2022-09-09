using LyricFinderCore.Models;

namespace LyricFinderCore.Interfaces
{
    /// <summary>
    /// This interface describes the methods to find the lyrics
    /// </summary>
    public interface ILyricFinder
    {
        /// <summary>
        /// /This method is used to get the lyric of the song and it may be null
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public Lyric? GetLyric(Song song);
    }
}
