namespace LyricFinderCore.Helpers
{
    public class WordCounter
    {
        public static int WordsCount(string content)
        {
            char[] WordSeperaters = new char[] { ',', ' ', '\t', '\n', '\r' };
            string[] words = content.Split(WordSeperaters);
            int count = 0;
            foreach (var word in words)
            {
                if(String.IsNullOrEmpty(word.Trim()))
                    continue;
                count++;
            }
            return count;
        }
    }
}
