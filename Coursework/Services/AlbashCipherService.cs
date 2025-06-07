using System.Collections.Generic;
using System.Linq;

namespace Coursework.Services
{
    public class AlbashCipherService
    {
        // Русский алфавит (только заглавные)
        private readonly string _russianAlphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        // Словари для шифрования и расшифровки (заглавные и строчные)
        private readonly Dictionary<char, char> _encryptionMap;
        private readonly Dictionary<char, char> _decryptionMap;
        private readonly Dictionary<char, char> _encryptionMapLower;
        private readonly Dictionary<char, char> _decryptionMapLower;

        public AlbashCipherService()
        {
            var reversedAlphabetUpper = new string(_russianAlphabet.Reverse().ToArray());

            _encryptionMap = new Dictionary<char, char>();
            _decryptionMap = new Dictionary<char, char>();
            _encryptionMapLower = new Dictionary<char, char>();
            _decryptionMapLower = new Dictionary<char, char>();

            for (int i = 0; i < _russianAlphabet.Length; i++)
            {
                char upperOriginal = _russianAlphabet[i];
                char upperEncrypted = reversedAlphabetUpper[i];

                // Заполняем словарь для заглавных букв
                _encryptionMap[upperOriginal] = upperEncrypted;
                _decryptionMap[upperEncrypted] = upperOriginal;

                // Заполняем словарь для строчных букв
                char lowerOriginal = char.ToLower(upperOriginal);
                char lowerEncrypted = char.ToLower(upperEncrypted);

                _encryptionMapLower[lowerOriginal] = lowerEncrypted;
                _decryptionMapLower[lowerEncrypted] = lowerOriginal;
            }
        }

        /// <summary>
        /// Зашифровывает текст по правилам Альбаша
        /// </summary>
        public string Encrypt(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (_encryptionMap.TryGetValue(c, out var encrypted))
                {
                    result[i] = encrypted;
                }
                else if (_encryptionMapLower.TryGetValue(c, out encrypted))
                {
                    result[i] = encrypted;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    result[i] = (char)('A' + ('Z' - c));
                }
                else if (c >= 'a' && c <= 'z')
                {
                    result[i] = (char)('a' + ('z' - c));
                }
                else
                {
                    result[i] = c; // Оставляем неизменным (например, пробелы, знаки)
                }
            }

            return new string(result);
        }

        /// <summary>
        /// Расшифровывает текст по правилам Альбаша
        /// </summary>
        public string Decrypt(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (_decryptionMap.TryGetValue(c, out var decrypted))
                {
                    result[i] = decrypted;
                }
                else if (_decryptionMapLower.TryGetValue(c, out decrypted))
                {
                    result[i] = decrypted;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    result[i] = (char)('A' + ('Z' - c));
                }
                else if (c >= 'a' && c <= 'z')
                {
                    result[i] = (char)('a' + ('z' - c));
                }
                else
                {
                    result[i] = c;
                }
            }

            return new string(result);
        }
    }
}