using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MedicApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.Controllers
{
    [Authorize]
    public class EnrolleeController : Controller
    {
        // GET: /<controller>/
        private const string basUrl = "https://localhost:44343/api/";
        private readonly string JsonFile = @"C:\Users\ONI David Adedoyin\source\repos\MedicApp\MedicApp\wwwroot\locations.json";
        public async Task<IActionResult> Index()
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Enrollee"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }

            return View(catList);
        }
        

        public async Task<IActionResult> Age(int age, int lgaId, DateTime startDate, DateTime endDate)
        {
            string dateTime = startDate.ToString();
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/age?age={age}&lgaId={lgaId}&startDate={startDate.ToString()}&endDate={endDate.ToString()}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            ViewBag.Query = age;
            ViewBag.Lga = await DisplayQueryLga(lgaId); ;
            string[] vs = DisplayQueryPeriod(startDate, endDate);
            ViewBag.StartDate = vs[0];
            ViewBag.EndDate = vs[1];
            GetLgaDetails(lgaId,startDate,endDate);

            return View("Index", catList);
        }

        public async void GetLgaDetails(int lgaId, DateTime startDate, DateTime endDate)
        {
            List<MapLgaModel> catList = new List<MapLgaModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Location/details?lgaId={lgaId}&startDate={startDate.ToString()}&endDate={endDate.ToString()}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<MapLgaModel>>(apiResponse);
                    }
                }
            }
            UpdateStats(catList);

        }

        public void UpdateStats(List<MapLgaModel> mapLgaModels)
        {   
            string json = System.IO.File.ReadAllText(JsonFile);

            try
            {
                var jObject = JObject.Parse(json);

                JArray featuresArrary = (JArray)jObject["features"];
                
                foreach (var item in mapLgaModels)
                {
                    for (int i = 0; i < featuresArrary.Count; i++)
                    {
                        var d = featuresArrary[i];

                        var jToken = d["properties"];
                        //var joken = jToken["id"].Value<int>();
                        if (jToken["id"].Value<int>() == item.LgaId)
                        {
                            jToken["femaleCount"] = item.FemaleEnrolleesCount;
                            jToken["maleCount"] = item.MaleEnrolleesCount;
                            jToken["patientCount"] = item.EnrolleesCount;
                            jToken["hospitalCount"] = item.HospitalCount;
                        }
                    }
                    

                    jObject["features"] = featuresArrary;
                    string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                    System.IO.File.WriteAllText(JsonFile, output);
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine("Update Error : " + ex.Message.ToString());
            }
        }


        private async Task<string> DisplayQueryLga(int lgaId)
        {
            if (lgaId == 0)
            {
                return "All Lgas";
            }
            LgaModel lga = await RetrieveLocationById(lgaId);
            return lga.Name;
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

        public async Task<IActionResult> AgeRange(int minAge, int maxAge, int lgaId, DateTime startDate, DateTime endDate)
        {
            if (minAge > maxAge)
            {
                maxAge = minAge + 150;
                //ModelState.AddModelError("","Minimum Age cant be greater than Maximum Age");
                //return RedirectToAction("","");
            }
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/agerange?minAge={minAge}&maxAge={maxAge}&lgaId={lgaId}&startDate={startDate.ToString()}&endDate={endDate.ToString()}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            ViewBag.Query = DisplayQueryAgeRange(minAge,maxAge);
            var s = await DisplayQueryLga(lgaId);
            ViewBag.Lga = s;
            string[] vs = DisplayQueryPeriod(startDate, endDate);
            ViewBag.StartDate = vs[0];
            ViewBag.EndDate = vs[1];
            GetLgaDetails(lgaId, startDate, endDate);
            return View("Index", catList);
        }

        private string DisplayQueryAgeRange(int minAge, int maxAge)
        {
            string queryAgeRange = "";
            if(minAge > maxAge)
            {
                maxAge = minAge + 150;
            }
            //minAge doesnt have a value and maxAge has a value
            if (minAge <= 0 && maxAge > 0)
            {
                minAge = 0;
            }
            //minAge has a value and maxAge doesnt have a value
            else if (minAge >= 0 && maxAge <= 0)
            {
                maxAge = 150;
            }
            //minAge doesnt have value and maxAge doesnt have a value
            else if (minAge <= 0 && maxAge <= 0)
            {
                minAge = 0;
                maxAge = 150;
            }
            queryAgeRange = $"between {minAge} years and {maxAge} years";
            return queryAgeRange;
        }

        public async Task<IActionResult> Gender(int genderId, int lgaId, DateTime startDate, DateTime endDate)
        {
            string gender = string.Empty;
            if (genderId == 0)
            {
                gender = "all";
            }
            else if (genderId == 1)
            {
                gender = "male";
            }
            else if (genderId == 2)
            {
                gender = "female";
            }
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/gender?gender={gender}&lgaId={lgaId}&startDate={startDate.ToString()}&endDate={endDate.ToString()}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }
            if(gender == "all")
            {
                gender = "either male or female";
            }
            ViewBag.Query = gender;
            var s = await DisplayQueryLga(lgaId);
            ViewBag.Lga = s;
            string[] vs = DisplayQueryPeriod(startDate, endDate);
            ViewBag.StartDate = vs[0];
            ViewBag.EndDate = vs[1];
            GetLgaDetails(lgaId, startDate, endDate);

            return View("Index", catList);
        }

        private string[] DisplayQueryPeriod(DateTime startTime, DateTime endTime)
        {
            string[] dateTimes = new string[2];
            string start, end;
            if (startTime.ToString() == "1/1/0001 12:00:00 AM" && endTime.ToString() != "1/1/0001 12:00:00 AM")
            {
                start = new DateTime(2018, 1, 1).ToLongDateString();
                end = endTime.ToLongDateString();
            }
            else if (startTime.ToString() != "1/1/0001 12:00:00 AM" && endTime.ToString() == "1/1/0001 12:00:00 AM")
            {
                start = startTime.ToLongDateString();
                end = DateTime.Now.ToLongDateString();
            }
            else if (startTime.ToString() == "1/1/0001 12:00:00 AM" && endTime.ToString() == "1/1/0001 12:00:00 AM")
            {
                start = new DateTime(2018, 1, 1).ToLongDateString();
                end = DateTime.Now.ToLongDateString();
            }
            else
            {
                start = startTime.ToLongDateString();
                end = endTime.ToLongDateString();
            }
            dateTimes[0] = start;
            dateTimes[1] = end;
            return dateTimes;
        }

        public async Task<IActionResult> LGA(int lgaId, DateTime startDate, DateTime endDate)
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/lga?lgaId={lgaId}&startDate={startDate.ToString()}&endDate={endDate.ToString()}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }

            var s = await DisplayQueryLga(lgaId);
            ViewBag.Lga = s;
            string[] vs = DisplayQueryPeriod(startDate, endDate);
            ViewBag.StartDate = vs[0];
            ViewBag.EndDate = vs[1];
            GetLgaDetails(lgaId, startDate, endDate);
            return View("Index", catList);
        }

        public async Task<IActionResult> Disease(int diseaseid, int lgaId, DateTime startDate, DateTime endDate)
        {
            List<EnrolleeModel> catList = new List<EnrolleeModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + $"Enrollee/disease?diseaseid={diseaseid}&lgaId={lgaId}&startDate={startDate.ToString()}&endDate={endDate.ToString()}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        catList = JsonConvert.DeserializeObject<List<EnrolleeModel>>(apiResponse);
                    }
                }
            }

            string[] vs = DisplayQueryPeriod(startDate, endDate);
            ViewBag.StartDate = vs[0];
            ViewBag.EndDate = vs[1];
            GetLgaDetails(lgaId, startDate, endDate);
            return View("Index", catList);
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

        public static async Task<List<LgaModel>> RetrieveAllLgas()
        {
            List<LgaModel> result = new List<LgaModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Location"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<List<LgaModel>>(apiResponse);
                    }
                }
            }
            return result;
        }

        public static async Task<LgaModel> RetrieveLocationById(int id)
        {
            LgaModel result = new LgaModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Location/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<LgaModel>(apiResponse);
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
