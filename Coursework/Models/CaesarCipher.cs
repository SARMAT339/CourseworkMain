using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Coursework.Models
{
    public class CaesarCipher
    {
        [Required(ErrorMessage = "Введите текст")]
        public string InputText { get; set; }

        [Required(ErrorMessage = "Введите ключ")]
        [Range(1, 33, ErrorMessage = "Ключ должен быть от 1 до 33")]
        public int Key { get; set; }

        public string OutputText { get; set; }

        public static string Encrypt(string input, int key)
        {
            return ShiftText(input, key);
        }

        public static string Decrypt(string input, int key)
        {
            return ShiftText(input, -key);
        }

        public static string ShiftText(string input, int shift)
        {
            StringBuilder result = new StringBuilder();

            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    char baseChar = char.IsUpper(c) ? 'А' : 'а';
                    char shifted = (char)((((c - baseChar) + shift + 33) % 33) + baseChar);
                    result.Append(shifted);
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }
    }
}
