using Coursework.Services;
using Microsoft.AspNetCore.Mvc;
using Coursework.Models;
using Coursework.Services;

namespace Coursework.Controllers
{
    public class AlbashController : Controller
    {
        private readonly AlbashCipherService _albashService;

        public AlbashController()
        {
            _albashService = new AlbashCipherService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Albash());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Albash model, string action)
        {
            if (action == "encrypt")
            {
                model.EncryptedText = _albashService.Encrypt(model.InputText);
            }
            else if (action == "decrypt")
            {
                // Используем Decrypt вместо Encrypt
                model.DecryptedText = _albashService.Decrypt(model.EncryptedText);
            }

            return View(model);
        }
    }
}