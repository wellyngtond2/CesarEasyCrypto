# CesarEasyCrypto
Cesar Encryption Implementation.<br/>
Implentação da Criptografia de Cesar

## Getting Started / Iniciando
Do a project rebuild and copy a generated DLL and import it into your project.<br/>
Faça um rebuild do projeto e cópie a DLL gerada e importe em seu projeto.

## Give an example / Exemplo

First add using in your class.<br/>
Primeiro adicione a referência em sua classe. <br/>
<code>using CesarCrypt.Service;</code>

*In your method*<br/>

### Encrypt / Encriptando
<pre>string NormalText = "abcd";
string EncryptedText = CryptoService.Encrypt(NormalText, 1);// output "bcde"
string EncryptedText2 = CryptoService.Encrypt(NormalText, 2);// output "cdef"</pre><br/>

### Decrypt / Descriptando
<pre>string NormalText = "abcd";
string EncryptedText = CryptoService.Decrypt(NormalText, 1);// output "zabc"
string EncryptedText2 = CryptoService.Decrypt(NormalText, 2);// output "yzab"</pre><br/>

## Authors

* **Wellyngton Borges** - [GitHub](https://github.com/wellyngtond2) - [Linkedin](https://www.linkedin.com/in/wellyngtonborges/)


## License

This project is licensed under GNU General Public License v2.0 - see the [LICENSE](LICENSE) file for details
