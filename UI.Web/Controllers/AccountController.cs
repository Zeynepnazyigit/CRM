using Core.Abstract.IServices;
using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService service;

        public AccountController(IAuthService service)
        {
            this.service = service;
        }

        // 🔒 Profil sayfası – sadece giriş yapanlar
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        // 🔓 LOGIN (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // 🔓 LOGIN (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.LoginAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Messages)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }

        // 🔓 REGISTER (GET)  ← BURASI ÇOK ÖNEMLİ
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // 🔓 REGISTER (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model, bool isAdmin = false)
        {
            if (ModelState.IsValid)
            {
                var result = await service.RegisterAsync(model, isAdmin);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Messages)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }

        // 🔒 LOGOUT
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await service.LogoutAsync();
            if (result.Success)
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
