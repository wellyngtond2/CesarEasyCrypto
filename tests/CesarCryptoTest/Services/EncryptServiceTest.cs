using CesarCrypt.Service;
using NUnit.Framework;

namespace CesarCryptoTest.Services
{
    [TestFixture(Category ="Encrypt Services")]
    public class EncryptServiceTest
    {
        [TestCase(26)]
        [TestCase(27)]
        [TestCase(50)]
        public void HouseNumberCannotGreaterThan25(int houseNumber)
        {
            var encriptedText = CryptoService.Encrypt("abcd", houseNumber);
            Assert.AreEqual(encriptedText,"abcd");
        }

        [TestCase(-1)]
        [TestCase(-20)]
        [TestCase(0)]
        [TestCase(50)]
        public void HouseNumberCannotByNegative(int houseNumber)
        {
            var encriptedText = CryptoService.Encrypt("abcd", houseNumber);
            Assert.AreEqual(encriptedText, "abcd");
        }

        [Test]
        public void InputTextCannotNull()
        {
            var encriptedText = CryptoService.Encrypt(null, 10);
            Assert.NotNull(encriptedText);
        }
    }
}
