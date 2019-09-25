using System.Security.Cryptography;
using System.Text;

namespace CesarCrypt.Funtions
{
    public static class StringFunctions
    {
        public static string StringToSha1(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            byte[] data = Encoding.ASCII.GetBytes(text);
            byte[] hashData = new SHA1Managed().ComputeHash(data);
            string hash = string.Empty;
            foreach (var b in hashData)
                hash += b.ToString("X2");
            
            return hash.ToLower();
        }
    }
}
