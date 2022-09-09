namespace LyricFinderCore.Models
{
    /// <summary>
    /// This is the model of Artist
    /// </summary>
    public class Artist : IEquatable<Artist>
    {
        /// <summary>
        /// The name of artist. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This is the constructor of Artist which has name 
        /// </summary>
        /// <param name="name"></param>
        public Artist(string name)
        {
            Name = name;
        }

        public bool Equals(Artist? obj)
        {
            if(obj == null) return false;
            //The name should not be empty
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(obj.Name))
                return false;
            return Name.Equals(obj.Name, StringComparison.CurrentCultureIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            if(obj == null) return false;
            return Equals(obj as Artist);
        }
    }
}
