using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace random_passcode.Controllers
{
    public class PasscodeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Survey()
        {
            if(HttpContext.Session.GetInt32("passNum") == null)
            {
                HttpContext.Session.SetInt32("passNum", 1);    
            }

            Random rand = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string pass  = new string(Enumerable.Repeat(chars, 14).Select(s => s[rand.Next(s.Length)]).ToArray());
            HttpContext.Session.SetString("randpass", pass);
            string result = HttpContext.Session.GetString("randpass");
            TempData["showpass"] = result;
            
            int? num = HttpContext.Session.GetInt32("passNum");
            ViewBag.passnum = (int)num;

            return View("index");
        }

        [HttpPost]
        [Route("generate")]
        public IActionResult Generate()
        {

            int? num = HttpContext.Session.GetInt32("passNum");
            int addnum = (int)num + 1;
            HttpContext.Session.SetInt32("passNum", addnum);


            return RedirectToAction("Survey");
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Survey");
        }

    }
}