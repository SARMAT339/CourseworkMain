namespace Coursework.Models
{
    public interface IRsaModel
    {
        public string Encrypt(string cipherText, string privateKey);
        public string Decrypt(string cipherText, string privateKey);
    }
}