using LyricFinderCore.Comparer;
using LyricFinderCore.Exceptions;
using LyricFinderCore.Interfaces;
using LyricFinderCore.Models;

namespace LyricFinderCore
{
    /// <summary>
    /// This is the lyric finder service to provide the methods about finding lyric.
    /// </summary>
    public class LyricFinderService
    {
        /// <summary>
        /// List of artist finders.
        /// </summary>
        private readonly List<IArtistFinder> _artistFinders;
        /// <summary>
        /// List of song finders.
        /// </summary>
        private readonly List<ISongFinder> _songFinders;
        /// <summary>
        /// List of lyric finder.
        /// </summary>
        private readonly List<ILyricFinder> _lyricFinders;

        /// <summary>
        /// This is the constructor of the lyric finder service
        /// </summary>
        /// <param name="artistFinders"></param>
        /// <param name="songFinders"></param>
        /// <param name="lyricFinders"></param>
        public LyricFinderService(List<IArtistFinder> artistFinders, List<ISongFinder> songFinders, List<ILyricFinder> lyricFinders)
        {
            _artistFinders = artistFinders;
            _songFinders = songFinders;
            _lyricFinders = lyricFinders;
        }
        /// <summary>
        /// This method is used to call the artist finder helps to find the artist by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Artist? SearchArtist(string name)
        {
            foreach (var finder in _artistFinders)
            {
                var result = finder.SearchArtist(name);
                if (result != null) return result;
            }

            return null;
        }
        /// <summary>
        /// This method is used to call the artist finder helps to search the artist by partial name
        /// </summary>
        /// <param name="partialName"></param>
        /// <param name="artistComparer"></param>
        /// <returns></returns>
        public List<Artist> SearchArtists(string partialName, IComparer<Artist>? artistComparer = null, int limit=20)
        {
            if (String.IsNullOrEmpty(partialName))
                throw new InvalidPartialNameException();
            HashSet<Artist> artistSet = new HashSet<Artist>();
            foreach (var finder in _artistFinders)
            {
                artistSet.UnionWith(finder.SearchArtists(partialName));
            }
            var result = artistSet.Distinct(ArtistComparer.Instance).ToList();
            if(artistComparer!=null)
                result.Sort(artistComparer);

            if (result.Count > limit)
                return result.GetRange(0, limit);
            else
                return result;
        }
        /// <summary>
        /// This method is used to call song finders to search the song by artist
        /// </summary>
        /// <param name="artist"></param>
        /// <param name="songComparer"></param>
        /// <returns></returns>
        public List<Song> SearchSongs(Artist artist, IComparer<Song>? songComparer = null)
        {
            HashSet<Song> songSet = new HashSet<Song>();
            foreach (var finder in _songFinders)
            {
                songSet.UnionWith(finder.SearchSongs(artist));
            }
            List<Song> result = songSet.Distinct(SongComparer.Instance).ToList();
            if(songComparer!=null)
                result.Sort(songComparer);
            return result;
        }
        /// <summary>
        /// This method is used to find the lyric
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public Lyric? GetLyric(Song song)
        {
            foreach (var finder in _lyricFinders)
            {
                var result = finder.GetLyric(song);
                if (result != null)
                    return result;
            }
            return null;
        }
    }
}