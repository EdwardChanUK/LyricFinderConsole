namespace LyricFinderCore.Models
{
    /// <summary>
    /// This is the model of Lyric.
    /// </summary>
    public class Lyric
    {
        /// <summary>
        /// This is the song of the lyric.
        /// </summary>
        public Song Song { get; set; }
        /// <summary>
        /// This is the lyric content.
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// The constructor of lyric which has song and content.
        /// </summary>
        /// <param name="song"></param>
        /// <param name="content"></param>
        public Lyric(Song song, string content)
        {
            Song = song;
            Content = content;
        }
    }
}
