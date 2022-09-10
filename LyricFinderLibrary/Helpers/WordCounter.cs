namespace LyricFinderCore.Helpers
{
    public class WordCounter
    {
        public static int WordsCount(string content)
        {
            char[] WordSeperaters = new char[] { ',', ' ', '\t', '\n', '\r' };
            string[] words = content.Split(WordSeperaters, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int count = 0;
            foreach (var word in words)
            {
                if(String.IsNullOrEmpty(word))
                    continue;
                if(word.Length==1 && Char.IsPunctuation(word.ToCharArray()[0]))
                    continue;
                count++;
            }
            return count;
        }
    }
}
