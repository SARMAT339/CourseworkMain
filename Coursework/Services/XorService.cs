using System.Text;

namespace Coursework.Services
{
    public class XorService
    {
        public string Encrypt(string plainText, string key)
        {
            byte[] data = Encoding.UTF8.GetBytes(plainText);
            byte[] secretKey = Encoding.UTF8.GetBytes(key);
            byte[] encrypted = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                encrypted[i] = (byte)(data[i] ^ secretKey[i % secretKey.Length]);
            }

            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string cipherText, string key)
        {
            try
            {
                byte[] data = Convert.FromBase64String(cipherText);
                byte[] secretKey = Encoding.UTF8.GetBytes(key);
                byte[] decrypted = new byte[data.Length];

                for (int i = 0; i < data.Length; i++)
                {
                    decrypted[i] = (byte)(data[i] ^ secretKey[i % secretKey.Length]);
                }

                return Encoding.UTF8.GetString(decrypted);
            }
            catch
            {
                return "Ошибка расшифровки. Проверьте правильность ключа.";
            }
        }
    }
}
