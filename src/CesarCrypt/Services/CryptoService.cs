using CesarCrypt.Enums;
using CesarCrypt.Models;
using System.Text.RegularExpressions;

namespace CesarCrypt.Service
{
    public static class CryptoService
    {
        public static string Decrypt(string EncryptText, int HouseNumber)
        {
            var decryptedText = "";
            foreach (var letter in EncryptText)
                decryptedText += letter.isNotLetter() ? letter.ToString() : GetCharDecrypted(HouseNumber, letter, EMode.Decrypting);
            return decryptedText;
        }

        public static string Encrypt(string EncryptText, int HouseNumber)
        {
            var decryptedText = "";
            foreach (var letter in EncryptText)
                decryptedText += letter.isNotLetter() ? letter.ToString() : GetCharDecrypted(HouseNumber, letter, EMode.Encrypt);
            return decryptedText;
        }

        private static bool isNotLetter(this char letter)
        {
            return Regex.IsMatch(letter.ToString(), (@"[^a-zA-Z0-9]")) || (letter == ' ');
        }

        private static string GetCharDecrypted(int houseNumber, char letter, EMode mode)
        {
            var index_decrypt = GetNewIndex(houseNumber, letter, mode);
            return new AlphabetModel().GetLetterByPosition(index_decrypt);
        }

        private static int GetNewIndex(int HouseNumber, char letter, EMode mode)
        {
            int index = GetIndexEncryptedText(letter);
            int newIndex = index - (mode == EMode.Decrypting ? HouseNumber : HouseNumber * -1);
            return mode == EMode.Decrypting ? (newIndex < 0 ? (26 - (newIndex * -1)) : newIndex) : (newIndex > 26 ? (newIndex - 26) : newIndex) ;
        }

        private static int GetIndexEncryptedText(char letter)
        {
            return new AlphabetModel().GetPositionByLetter(letter);
        }
    }
}
