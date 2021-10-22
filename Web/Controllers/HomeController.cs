using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Database.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> mLogger;

        public HomeController(ILogger<HomeController> logger, WebDbContext context)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }
            mLogger = logger;
            foreach (var sample in context.Sample)
            {
                Console.WriteLine(sample);
            }
        }

        public IActionResult Index()
        {
            mLogger.LogInformation(nameof(Index));
            return View();
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
