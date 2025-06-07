using Coursework.Models;
using Coursework.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.Controllers
{
    public class XorController : Controller
    {
        private readonly XorService _xorService;

        public XorController(XorService xorService)
        {
            _xorService = xorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Xor());
        }

        [HttpPost]
        public IActionResult Index(Xor model)
        {
            if (!string.IsNullOrEmpty(model.InputText) && !string.IsNullOrEmpty(model.Key))
            {
                model.EncryptedText = _xorService.Encrypt(model.InputText, model.Key);

                if (!string.IsNullOrEmpty(model.EncryptedText))
                {
                    model.DecryptedText = _xorService.Decrypt(model.EncryptedText, model.Key);
                }
            }

            return View(model);
        }
    }
}
