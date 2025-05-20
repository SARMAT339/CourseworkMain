using Coursework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Coursework.Controllers
{
    public class RsaController : Controller
    {
        readonly IRsaModel rsaModel;

        public RsaController(IRsaModel rsaModel)
        {
            this.rsaModel = rsaModel;
        }

        // Отображение формы
        public ActionResult Index()
        {
            var model = new RsaModel();
            GenerateKeys(model); // Генерируем ключи по умолчанию
            return View(model);
        }

        // Обработка формы
        [HttpPost]
        public ActionResult Index(RsaModel model, string action)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                if (action == "encrypt")
                {
                    model.OutputText = rsaModel.Encrypt(model.InputText, model.PublicKey);
                }
                else if (action == "decrypt")
                {
                    model.OutputText = rsaModel.Decrypt(model.OutputText, model.PrivateKey);
                }
                else
                {
                    ModelState.AddModelError("", "Выберите действие: шифрование или расшифрование");
                }
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

        // Генерация пары ключей
        public ActionResult GenerateKeys(RsaModel model)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                model.PublicKey = rsa.ToXmlString(false); // Только открытый ключ
                model.PrivateKey = rsa.ToXmlString(true);  // Открытый + закрытый ключ
            }
            return View("Index", model);
        }
    }
}
