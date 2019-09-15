using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MedicApp.dummyinput.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.dummyinput.Controllers
{
    public class DiseaseController : Controller
    {
        private const string basUrl = "https://localhost:44343/api/";
        public async Task<IActionResult> Index()
        {
            List<DiseaseModel> DiseaseList = new List<DiseaseModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Disease"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        DiseaseList = JsonConvert.DeserializeObject<List<DiseaseModel>>(apiResponse);
                    }
                }
            }
            return View(DiseaseList);
        }

        public IActionResult AddDisease()
        {
            DiseaseModel Disease = new DiseaseModel();
            return View(Disease);

        }
        
        [HttpPost]
        public async Task<IActionResult> AddDisease(DiseaseModel diseaseModel)
        {
            //diseaseModel.DateDiagnosed = DateTime.Now;
            DiseaseModel Disease = new DiseaseModel();
            
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(diseaseModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(basUrl + "Disease", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //HttpResponseMessage mssg = new HttpResponseMessage();
                        //mssg.StatusCode = System.Net.HttpStatusCode.OK;
                        Disease = JsonConvert.DeserializeObject<DiseaseModel>(apiResponse);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //GetById
        public async Task<IActionResult> UpdateDisease(int id)
        {
            DiseaseModel Disease = new DiseaseModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Disease/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Disease = JsonConvert.DeserializeObject<DiseaseModel>(apiResponse);
                    }
                }
            }
            return View(Disease);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDisease(DiseaseModel Disease)
        {
            DiseaseModel anim = new DiseaseModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Disease), Encoding.UTF8, "application/json");
                    string url = basUrl + "Disease/" + Disease.Id;
                    using (var response = await httpClient.PutAsync(url, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        anim = JsonConvert.DeserializeObject<DiseaseModel>(apiResponse);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.DeleteAsync(basUrl + "Disease/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ViewEnrolleeOfDisease(int diseaseId)
        {
            List<DiseaseModel> catList = new List<DiseaseModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Disease/DiseasesOfEnrollee?enrolleeId={diseaseId}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<DiseaseModel>>(apiResponse);
                    }
                }
            }
            ViewBag.EnrolleeId = diseaseId;
            return View(catList);
        }
    }
}
