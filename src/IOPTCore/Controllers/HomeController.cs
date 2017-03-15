using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;
using IOPTCore.Models;
using Newtonsoft.Json;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace IOPTCore.Controllers
{
    public class HomeController : Controller
    {
        ILoggerFactory _loggerFactory;
        DataContext db;
        public HomeController(ILoggerFactory loggerFactory, DataContext context)
        {
            db = context;
            _loggerFactory = loggerFactory;
        }
        public IActionResult Index()
        {
            ViewBag.snapModels =Snapshot.current.models;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, string password)
        {
            //if (ModelState.IsValid)
            //{
            _loggerFactory.CreateLogger("logger").LogInformation(login+" "+password);
            User user = db.Users.FirstOrDefault(u => u.login == login && u.password == password);
            if (user != null)
            {
                await Authenticate(login); // аутентификация

                return Redirect("~/Main");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            //}
            ViewData["eггoг"] = "Некорректные логин и(или) пароль";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("session");
            return Redirect("~/");
        }
        public IActionResult Error()
        {
            return View();
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
                };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.Authentication.SignInAsync("session", new ClaimsPrincipal(id));
        }
    }
}
