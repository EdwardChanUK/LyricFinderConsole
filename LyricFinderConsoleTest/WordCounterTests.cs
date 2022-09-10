using LyricFinderCore.Finders.LyricsOvh;
using LyricFinderCore.Helpers;
using LyricFinderCore.Models;

namespace LyricFinderConsoleTest
{
    /// <summary>
    /// This class describes the unit test around the word counter
    /// </summary>
    [TestFixture]
    public class WordCounterTests
    {
        /// <summary>
        /// This is the unit tests for different seperators.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="expectedCount"></param>
        /// <returns></returns>
        [TestCase("Hello World", 2)]
        [TestCase("Hello,World", 2)]
        [TestCase("Hello\tWorld", 2)]
        [TestCase("Hello\nWorld", 2)]
        [TestCase("Hello\rWorld", 2)]
        [TestCase("Hello\n\rWorld", 2)]
        [TestCase("Hello ? World", 2)]
        public void WordsCount_InputContent_WordsCanBeCount(string content, int expectedCount)
        {
            //arrange
            //act
            var result = WordCounter.WordsCount(content);
            //assert
            Assert.That(result, Is.EqualTo(expectedCount));
        }

    }
}
