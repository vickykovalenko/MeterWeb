using MeterWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace MeterWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBLibraryContext _context;
        private readonly ClaimsPrincipal _user;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<HomeController> _logger;

        public HomeController(DBLibraryContext context, UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            _context = context;
            _accessor = accessor;
            _user = accessor.HttpContext.User;
            _userManager = userManager;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
             var userid = _userManager.GetUserId(HttpContext.User);
            //return user id which has flats  
            var model = _context.Flats.Where(f => f.UserId == userid).ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
