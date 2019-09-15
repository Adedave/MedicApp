using System.Net.Http;
using System.Threading.Tasks;
using MedicApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.Controllers
{
    public class AuthController : Controller
    {
        private const string basUrl = "https://localhost:44343/api/";


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            TempData["failed"] = null;

            if (!ModelState.IsValid)
            {
                TempData["failed"] = "failed";
                model.Password = string.Empty;
                return View(model);
            }

            UserModel user = new UserModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(basUrl + $"Management/username?username={model.Username.ToLower()}&password={model.Password}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<UserModel>(apiResponse);
                }
            }


            if (user == null)
            {
                TempData["failed"] = "failed";
                ModelState.AddModelError("Error", "Username or password is invalid");
                return View(model);
            }

            TempData["Username"] = model.Username.ToUpper();
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        public IActionResult Login()
        {

            var loginModel = new LoginViewModel();

            return View(loginModel);
        }
    }
}
