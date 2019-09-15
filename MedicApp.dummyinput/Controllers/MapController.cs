using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicApp.dummyinput.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.dummyinput.Controllers
{
    public class MapController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<MapLgaModel> mapLgaModel = new List<MapLgaModel>();
            //number of patients enrolled in each lga
            //number of male and females
            //number of hospitals
            //top three diseases are ...
            return View();
        }
    }
}
