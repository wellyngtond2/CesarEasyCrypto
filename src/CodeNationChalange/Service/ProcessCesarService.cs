using CesarCrypt.Service;
using CesarCrypt.Funtions;
using CodeNationChalange.Infra;
using CodeNationChalange.Models;

namespace CodeNationChalange.Service
{
    public class ProcessCesarService
    {
        public static void Process()
        {   
            var textEncriptado = CryptoService.Encrypt("wellyngton", 1);
            var decryptedText = CryptoService.Decrypt(textEncriptado, 1);
            var file = FilesRepository.OpenFile();
            file.decifrado = decryptedText;
            FilesRepository.SaveFile(file);
            var decryptedToSha1 = decryptedText.StringToSha1();
            file = FilesRepository.OpenFile();
            file.resumo_criptografico = decryptedToSha1;
            FilesRepository.SaveFile(file);
            var fileByte= FilesRepository.GetFileString("answer.json");
            var responseSend = WebService<ResponseObject>.Post(fileByte);
        }
    }
}
