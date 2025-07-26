using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class ErrorController : Controller
    {

public IActionResult Page(int? code = null)
        {
            ViewBag.Code = "خطا";
            if (code.HasValue)
            {
                ViewBag.Code = code.Value.ToString();
            }

            return View();
        }
    }
}
