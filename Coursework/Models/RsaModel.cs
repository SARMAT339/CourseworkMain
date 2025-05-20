using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Coursework.Models
{
    public class RsaModel : IRsaModel
    {
        [Required(ErrorMessage = "Введите текст для шифрования")]
        [Display(Name = "Текст для шифрования")]
        public string InputText { get; set; }

        [Required(ErrorMessage = "Введите открытый ключ")]
        [Display(Name = "Открытый ключ (XML)")]
        public string PublicKey { get; set; }

        [Required(ErrorMessage = "Введите закрытый ключ")]
        [Display(Name = "Закрытый ключ (XML)")]
        public string PrivateKey { get; set; }

        [Display(Name = "Результат")]
        public string OutputText { get; set; }

        public string Encrypt(string plainText, string publicKey)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                byte[] encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), true);
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        // Метод расшифрования
        public string Decrypt(string cipherText, string privateKey)
        {
            if (string.IsNullOrEmpty(cipherText)) return string.Empty;

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                byte[] decryptedBytes = rsa.Decrypt(cipherBytes, true);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
