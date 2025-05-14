using Coursework.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.Controllers
{
    public class CaesarCipherController : Controller
    {
        public ActionResult Index()
        {
            return View(new CaesarCipher());
        }

        [HttpPost]
        public ActionResult Index(CaesarCipher model, string action)
        {
            if (action == "encrypt")
            {
                model.OutputText = CaesarCipher.Encrypt(model.InputText, model.Key);
            }
            else if (action == "decrypt")
            {
                model.OutputText = CaesarCipher.Decrypt(model.InputText, model.Key);
            }
            else
            {
                model.OutputText = "Ошибка: выберите действие.";
            }

            return View(model);
        }
    }
}
