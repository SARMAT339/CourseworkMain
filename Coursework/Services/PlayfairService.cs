using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayfairCipherApp.Services
{
    public class PlayfairService
    {
        private const string EnglishAlphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
        private const string RussianAlphabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";

        public string Encrypt(string text, string language = "en")
        {
            var matrix = GetMatrix(language);
            return Process(text, matrix, encrypt: true);
        }

        public string Decrypt(string text, string language = "en")
        {
            var matrix = GetMatrix(language);
            return Process(text, matrix, encrypt: false);
        }

        private char[,] GetMatrix(string language)
        {
            string alphabet = language == "ru" ? RussianAlphabet : EnglishAlphabet;
            string key = "SECRETKEY" + alphabet; // Пример ключа
            StringBuilder uniqueKey = new StringBuilder();

            foreach (char c in key.ToUpper())
            {
                if (!uniqueKey.ToString().Contains(c) && alphabet.Contains(c))
                {
                    uniqueKey.Append(c);
                }
            }

            foreach (char c in alphabet)
            {
                if (!uniqueKey.ToString().Contains(c))
                {
                    uniqueKey.Append(c);
                }
            }

            int size = (int)Math.Sqrt(uniqueKey.Length);
            char[,] matrix = new char[size, size];
            int index = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = uniqueKey[index++];
                }
            }

            return matrix;
        }

        private string Process(string text, char[,] matrix, bool encrypt)
        {
            int size = (int)Math.Sqrt(matrix.Length);
            StringBuilder result = new StringBuilder();
            text = PrepareText(text, matrix, encrypt);

            for (int i = 0; i < text.Length; i += 2)
            {
                var pos1 = GetPosition(matrix, text[i]);
                var pos2 = GetPosition(matrix, text[i + 1]);

                if (pos1.Item1 == pos2.Item1) // одна строка
                {
                    result.Append(matrix[pos1.Item1, encrypt ? (pos1.Item2 + 1) % size : (pos1.Item2 + size - 1) % size]);
                    result.Append(matrix[pos2.Item1, encrypt ? (pos2.Item2 + 1) % size : (pos2.Item2 + size - 1) % size]);
                }
                else if (pos1.Item2 == pos2.Item2) // один столбец
                {
                    result.Append(matrix[encrypt ? (pos1.Item1 + 1) % size : (pos1.Item1 + size - 1) % size, pos1.Item2]);
                    result.Append(matrix[encrypt ? (pos2.Item1 + 1) % size : (pos2.Item1 + size - 1) % size, pos2.Item2]);
                }
                else // прямоугольник
                {
                    result.Append(matrix[pos1.Item1, pos2.Item2]);
                    result.Append(matrix[pos2.Item1, pos1.Item2]);
                }
            }

            return result.ToString();
        }

        private (int, int) GetPosition(char[,] matrix, char c)
        {
            int size = (int)Math.Sqrt(matrix.Length);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] == c)
                        return (i, j);
                }
            }
            return (-1, -1);
        }

        private string PrepareText(string text, char[,] matrix, bool encrypt)
        {
            text = text.ToUpper().Replace(" ", "").Replace("J", "I");
            text = text.Replace("Ё", "Е").Replace("Й", "И");

            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                if ("ABCDEFGHIJKLMNOPQRSTUVWXYZАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".Contains(c))
                    sb.Append(c);
            }

            text = sb.ToString();

            for (int i = 0; i < text.Length - 1; i += 2)
            {
                if (text[i] == text[i + 1])
                {
                    text = text.Insert(i + 1, "X");
                }
            }

            if (text.Length % 2 != 0)
                text += "X";

            return text;
        }
    }
}