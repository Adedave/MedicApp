using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MedicApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.Controllers
{
    public class EnrolleeController : Controller
    {
        // GET: /<controller>/
        private const string basUrl = "https://localhost:44343/api/";
        public async Task<IActionResult> Index()
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "User"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            var newEnrolleeList = await AddEnrolleeDiseases(catList);

            return View(newEnrolleeList);
        }
        

        public async Task<IActionResult> Age(int age)
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Enrollee/age?age=" + age))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            var newEnrolleeList = await AddEnrolleeDiseases(catList);
            
            return View("Index", newEnrolleeList);
        }

        private async Task<List<EnrolleeModel>> AddEnrolleeDiseases(List<EnrolleeModel> catList)
        {
            foreach (var item in catList)
            {
                item.Diseases = await GetEnrolleeDiseases(item.Id);
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

        public async Task<IActionResult> AgeRange(int minAge, int maxAge)
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/agerange?minAge={minAge}&maxAge={maxAge}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            var newEnrolleeList = await AddEnrolleeDiseases(catList);

            return View("Index", newEnrolleeList);
        }

        public async Task<IActionResult> Gender(string gender)
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/gender?gender={gender}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            var newEnrolleeList = await AddEnrolleeDiseases(catList);

            return View("Index", newEnrolleeList);
        }

        public async Task<IActionResult> LGA(string lga)
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/lga?lga={lga}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            var newEnrolleeList = await AddEnrolleeDiseases(catList);

            return View("Index", newEnrolleeList);
        }

        public async Task<IActionResult> Disease(int diseaseid)
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/disease?diseaseid={diseaseid}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            var newEnrolleeList = await AddEnrolleeDiseases(catList);

            return View("Index", newEnrolleeList);
        }

        public static async Task<List<DiseaseModel>> RetrieveAllDiseases()
        {
            List<DiseaseModel> result = new List<DiseaseModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Disease"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<DiseaseModel>>(apiResponse);
                    }
                }
            }
            return result;
        }

        public static async Task<DiseaseModel> RetrieveDiseaseById(int id)
        {
            DiseaseModel result = new DiseaseModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Disease/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<DiseaseModel>(apiResponse);
                    }
                }
            }
            return result;
        }
    }
}
