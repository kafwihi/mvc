//https://www.youtube.com/watch?v=hb7iJ-mt3_8
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;
namespace mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(){
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model){
            if(ModelState.IsValid){
                //to do
            }
            return View();
        }
    }
}