using AppsettingManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AppsettingManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private TwilioSetting _twilioSetting;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _twilioSetting = new TwilioSetting();
            configuration.GetSection("Twilio").Bind(_twilioSetting);
        }

        public IActionResult Index()
        {
            ViewBag.SendGridKey = _configuration.GetValue<string>("SendGridKey");
            ViewBag.TwilioKey = _configuration.GetValue<string>("Twilio:TwilioKey");
            ViewBag.TwilioSID = _configuration.GetValue<string>("Twilio:TwilioSID");

            ViewBag.PhoneNumber = _twilioSetting.PhoneNumber;
            ViewBag.ConnectonString = _configuration.GetConnectionString("AppSettingManagerDb");
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
