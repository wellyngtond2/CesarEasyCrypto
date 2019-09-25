using CesarCrypt.Funtions;
using NUnit.Framework;

namespace CesarCryptoTest.Functions
{
    [TestFixture(Category = "StringFunctions")]
    public class StringFunctionTest
    {
        [TestCase("textExample")]
        [TestCase(" ")]
        [TestCase(null)]
        [TestCase("1")]
        public void StringToCharNotNull(string text)
        {
            var alphabet = text.StringToSha1();
            Assert.NotNull(alphabet);
        }
    }
}
