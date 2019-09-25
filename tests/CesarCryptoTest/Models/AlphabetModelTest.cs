using CesarCrypt.Models;
using NUnit.Framework;

namespace CesarCryptoTest.Models
{
    [TestFixture(Category ="AlphabetModel")]
    public class AlphabetModelTest
    {
        [TestCase(-1)]
        [TestCase(27)]
        [TestCase(null)]
        public void LetterCannotByNull(int position)
        {
            var alphabet = new AlphabetModel();
            string letter = alphabet.GetLetterByPosition(position);
            Assert.NotNull(letter);
        }

        [TestCase(' ')]
        [TestCase('*')]
        [TestCase('1')]
        public void PositionOfLetterCannotByNull(char letter)
        {
            var alphabet = new AlphabetModel();
            var position = alphabet.GetPositionByLetter(letter);
            Assert.NotNull(position);
        }
    }
}
