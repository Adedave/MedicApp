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
    public class EnrolleeController : Controller
    {
        private const string basUrl = "https://localhost:44343/api/";
        public async Task<IActionResult> Index()
        {
            //PopulateEnrolleeTable();
            List<EnrolleeModel> enrolleeList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Enrollee"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        enrolleeList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            //AssignDiseaseInternally(enrolleeList);
            //var newEnrolleeList = await AddEnrolleeDiseases(enrolleeList);

            return View(enrolleeList);
        }
        private void AssignDiseaseInternally(List<EnrolleeModel> ele)
        {
            int[] diseaseIds = new int[]
                { 1,2,3,4,5,6,7,8};
            int diseaseId = 0;
            for (int i = 0; i < ele.Count(); i++)
            {
                EnrolleeDiseaseModel enrolleeDisease = new EnrolleeDiseaseModel();
                if (diseaseId == diseaseIds.Length)
                {
                    diseaseId = 0;
                }
                enrolleeDisease.DiseaseId = diseaseId;
                diseaseId++;
                i+=5;
                if (true)
                {

                }
                enrolleeDisease.EnrolleeId = ele[i].Id;

                AssignDisease(enrolleeDisease);
            }
        }

        public void PopulateEnrolleeTable()
        {
            int genderId = 0;
            int heightsId = 0;
            int weightsId = 0;
            int age = 15;
            List<string> gender = new List<string>()
            {
                "male",
                "female"
            };

            //int[] HospitalIds = new int[] {
            //    1,2,3,4,5,6,7,8,9,10,11,12,
            //    13,14,15,16,17,18,19
            //};
            int hospitalIds = 1;
            double[] heights = new double[] { 125, 135, 145, 156, 167, 176, 186, 194 };
            double[] weights = new double[] { 50, 60, 70, 75, 67, 88, 98, 100 };
            for (int i = 0; i < 380; i++)
            {
                EnrolleeModel enrolleeModel = new EnrolleeModel();
                if (age > 100)
                {
                    age = 15;
                }
                enrolleeModel.Age = age;
                age += 5;

                if (hospitalIds > 19)
                {
                    hospitalIds = 1;
                }
                enrolleeModel.HospitalId = hospitalIds;
                hospitalIds++;

                if (genderId > 1)
                {
                    genderId = 0;
                }
                enrolleeModel.Gender = gender[genderId];
                genderId++;

                if (heightsId == heights.Length)
                {
                    heightsId = 0;
                }
                enrolleeModel.Height = heights[heightsId];
                heightsId++;

                if (weightsId == weights.Length)
                {
                    weightsId = 0;
                }
                enrolleeModel.Weight = weights[weightsId];
                weightsId++;

                 AddEnrolleeInternal(enrolleeModel);
            }

        }

        private async void AddEnrolleeInternal(EnrolleeModel EnrolleeModel)
        {
            EnrolleeModel Enrollee = new EnrolleeModel();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(EnrolleeModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(basUrl + "Enrollee", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //HttpResponseMessage mssg = new HttpResponseMessage();
                        //mssg.StatusCode = System.Net.HttpStatusCode.OK;
                        Enrollee = JsonConvert.DeserializeObject<EnrolleeModel>(apiResponse);
                    }
                }
            }
        }

        private async Task<List<EnrolleeModel>> AddEnrolleeDiseases(List<EnrolleeModel> catList)
        {
            foreach (var item in catList)
            {
                //item.Diseases = await GetEnrolleeDiseases(item.Id);
            }
            return catList;
        }

        public async Task<List<DiseaseModel>> GetEnrolleeDiseases(int enrolleeId)
        {
            List<DiseaseModel> catList = new List<DiseaseModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Disease/DiseasesOfEnrollee?enrolleeId={enrolleeId}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<DiseaseModel>>(apiResponse);
                    }
                }
            }
            return catList;
        }

        public async Task<IActionResult> ViewEnrolleeDiseases(int enrolleeId)
        {
            List<DiseaseModel> catList = new List<DiseaseModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Disease/DiseasesOfEnrollee?enrolleeId={enrolleeId}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<DiseaseModel>>(apiResponse);
                    }
                }
            }
            ViewBag.EnrolleeId = enrolleeId;
            return View(catList);
        }

        public IActionResult AddEnrollee()
        {
            EnrolleeModel Enrollee = new EnrolleeModel();
            return View(Enrollee);

        }

        //[HttpGet("{id}")]
        public IActionResult AssignDisease(int id)
        {
            EnrolleeDiseaseModel enrollee = new EnrolleeDiseaseModel()
            {
                EnrolleeId = id
            };
            return View(enrollee);
        }

        [HttpPost]
        public async void AssignDisease(EnrolleeDiseaseModel enrolleeDisease)
        {
            EnrolleeModel Enrollee = new EnrolleeModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(enrolleeDisease), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(basUrl + "Enrollee/assign-disease", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //HttpResponseMessage mssg = new HttpResponseMessage();
                        //mssg.StatusCode = System.Net.HttpStatusCode.OK;
                        Enrollee = JsonConvert.DeserializeObject<EnrolleeModel>(apiResponse);
                    }
                }
            }
            //ViewBag.Succ = "Successful";
            //return View();
        }




        [HttpPost]
        public async Task<IActionResult> AddEnrollee(EnrolleeModel EnrolleeModel)
        {
            EnrolleeModel Enrollee = new EnrolleeModel();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(EnrolleeModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(basUrl + "Enrollee", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //HttpResponseMessage mssg = new HttpResponseMessage();
                        //mssg.StatusCode = System.Net.HttpStatusCode.OK;
                        Enrollee = JsonConvert.DeserializeObject<EnrolleeModel>(apiResponse);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LGA(int id)
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/lga?lgaId={id}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            var newEnrolleeList = await AddEnrolleeDiseases(catList);

            return View("Index", newEnrolleeList);
        }

        //GetById
        public async Task<IActionResult> UpdateEnrollee(int id)
        {
            EnrolleeModel Enrollee = new EnrolleeModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Enrollee/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Enrollee = JsonConvert.DeserializeObject<EnrolleeModel>(apiResponse);
                    }
                }
            }
            return View(Enrollee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEnrollee(EnrolleeModel enrollee)
        {
            EnrolleeModel anim = new EnrolleeModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(enrollee), Encoding.UTF8, "application/json");
                    string url = basUrl + "Enrollee/" + enrollee.Id;
                    using (var response = await httpClient.PutAsync(url, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        anim = JsonConvert.DeserializeObject<EnrolleeModel>(apiResponse);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        public async Task<IActionResult> DeleteEnrollee(int id)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.DeleteAsync(basUrl + "Enrollee/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
