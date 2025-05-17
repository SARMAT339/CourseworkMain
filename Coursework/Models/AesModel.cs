using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Coursework.Models
{
    public class AesModel
    {
        [Required(ErrorMessage = "Введите текст для обработки")]
        [Display(Name = "Текст")]
        public string InputText { get; set; }

        [Required(ErrorMessage = "Введите ключ в формате Base64")]
        [RegularExpression(@"^[A-Za-z0-9+/=]+$", ErrorMessage = "Недопустимый формат ключа. Используйте Base64.")]
        [Display(Name = "Ключ (Base64)")]
        public string Key { get; set; }

        [Required(ErrorMessage = "Введите IV в формате Base64 (для режима CBC и т.д.)")]
        [RegularExpression(@"^[A-Za-z0-9+/=]+$", ErrorMessage = "Недопустимый формат IV. Используйте Base64.")]
        [Display(Name = "IV (Base64)")]
        public string IV { get; set; }

        [Display(Name = "Результат")]
        public string OutputText { get; set; }

        [Required(ErrorMessage = "Выберите размер ключа")]
        [Range(128, 256, ErrorMessage = "Допустимые значения: 128, 192, 256")]
        [Display(Name = "Размер ключа (бит)")]
        public int KeySize { get; set; } = 256;


        public static string Encrypt(string plainText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(
                        Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        // Метод расшифрования
        public static string Decrypt(string cipherText, byte[] key, byte[] iv)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (var decryptor = aes.CreateDecryptor())
                {
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(
                        cipherBytes, 0, cipherBytes.Length);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
