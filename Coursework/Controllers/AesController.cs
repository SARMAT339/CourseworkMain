using Coursework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Coursework.Controllers
{
    public class AesController : Controller
    {
        public ActionResult Index()
        {
            return View(new AesModel());
        }

        [HttpPost]
        public ActionResult Index(AesModel model, string action)
        {
            // Проверка валидации модели
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Преобразование Base64 в байты
                byte[] keyBytes = Convert.FromBase64String(model.Key);
                byte[] ivBytes = Convert.FromBase64String(model.IV);

                // Проверка соответствия размера ключа
                if (keyBytes.Length * 8 != model.KeySize)
                {
                    ModelState.AddModelError("Key", $"Длина ключа должна быть {model.KeySize / 8} байт");
                    return View(model);
                }

                if (action == "encrypt")
                {
                    model.OutputText = AesModel.Encrypt(model.InputText, keyBytes, ivBytes);
                }
                else if (action == "decrypt")
                {
                    model.OutputText = AesModel.Decrypt(model.OutputText, keyBytes, ivBytes);
                }
                else
                {
                    ModelState.AddModelError("", "Выберите действие: шифрование или расшифрование");
                }
            }
            catch (FormatException)
            {
                ModelState.AddModelError("Key", "Неверный формат Base64 для ключа или IV");
            }
            catch (CryptographicException ex)
            {
                ModelState.AddModelError("", "Ошибка шифрования: " + ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Произошла ошибка: " + ex.Message);
            }

            return View(model);
        }
    }
}
