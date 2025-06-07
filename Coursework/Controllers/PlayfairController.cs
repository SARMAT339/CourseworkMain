using Coursework.Models;
using Microsoft.AspNetCore.Mvc;
using PlayfairCipherApp.Services;

namespace PlayfairCipherApp.Controllers
{
    public class PlayfairController : Controller
    {
        private readonly PlayfairService _playfairService;

        public PlayfairController(PlayfairService playfairService)
        {
            _playfairService = playfairService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Playfair());
        }

        [HttpPost]
        public IActionResult Index(Playfair model, string language)
        {
            if (!string.IsNullOrEmpty(model.InputText))
            {
                model.EncryptedText = _playfairService.Encrypt(model.InputText, language);
                model.DecryptedText = _playfairService.Decrypt(model.EncryptedText, language);
            }

            return View(model);
        }
    }
}