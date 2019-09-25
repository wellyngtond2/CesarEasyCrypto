using CesarCrypt.Service;
using NUnit.Framework;

namespace CesarCryptoTest.Services
{
    [TestFixture(Category ="Decrypt Services")]
    public class DecryptServiceTest
    {
        [TestCase(26)]
        [TestCase(27)]
        [TestCase(50)]
        public void HouseNumberCannotGreaterThan25(int houseNumber)
        {
            var encriptedText = CryptoService.Decrypt("abcd", houseNumber);
            Assert.AreEqual(encriptedText,"abcd");
        }

        [TestCase(-1)]
        [TestCase(-20)]
        [TestCase(0)]
        [TestCase(50)]
        public void HouseNumberCannotByNegative(int houseNumber)
        {
            var encriptedText = CryptoService.Decrypt("abcd", houseNumber);
            Assert.AreEqual(encriptedText, "abcd");
        }

        [Test]
        public void InputTextCannotNull()
        {
            var encriptedText = CryptoService.Decrypt(null, 10);
            Assert.NotNull(encriptedText);
        }
    }
}
