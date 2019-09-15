using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MedicApp.dummyinput.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.dummyinput.Controllers
{
    public class HospitalController : Controller
    {
        private const string basUrl = "https://localhost:44343/api/";
        public async Task<IActionResult> Index()
        {
            //AddHospitals();
            List<HospitalModel> hospitalList = new List<HospitalModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Hospital"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        hospitalList = JsonConvert.DeserializeObject<List<HospitalModel>>(apiResponse);
                    }
                }
            }
            return View(hospitalList);
        }

        public IActionResult AddHospital()
        {

            HospitalModel hospital = new HospitalModel();
             return View(hospital);

        }

        public static async Task<IEnumerable<LocationModel>> RetrieveAllLocations()
        {
            List<LocationModel> hospitalList = new List<LocationModel>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Location"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        hospitalList = JsonConvert.DeserializeObject<List<LocationModel>>(apiResponse);
                    }
                }
            }
            return hospitalList;
        }

        [HttpPost]
        public async Task<IActionResult> AddHospital(string locationId, HospitalModel hospitalModel)
        {
            if (string.IsNullOrEmpty(locationId) && string.IsNullOrEmpty(hospitalModel.Name))
            {
                ModelState.AddModelError("LocationOrHospitalName","Location or HospitalName is not provided");
                return View(hospitalModel);
            }
            HospitalModel hospital = new HospitalModel();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(hospitalModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(basUrl + "Hospital", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //HttpResponseMessage mssg = new HttpResponseMessage();
                        //mssg.StatusCode = System.Net.HttpStatusCode.OK;
                        hospital = JsonConvert.DeserializeObject<HospitalModel>(apiResponse);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public void AddLocation()
        {
            List<string> lgas = new List<string>()
            {
                "Alimosho",
                "Ajeromi-Ifelodun",
                "Kosofe",
                "Mushin",
                "Oshodi-Isolo",
                "Ojo",
                "Ikorodu",
                "Surulere",
                "Agege",
                "Ifako-Ijaiye",
                "Somolu",
                "Amuwo-Odofin",
                "Lagos Mainland",
                "Ikeja",
                "Eti-Osa",
                "Badagry",
                "Apapa",
                "Lagos Island",
                "Epe",
                "Ibeju-Lekki",
                "All Lgas",
            };
            double[] longitudes = new double[] {
                3.257,3.3339,3.3784,3.3414,
                3.3246,3.1579,3.5105,3.3439,
                3.3209,3.2885,3.3842,3.2885,
                3.3781,3.3515,3.6015,2.8876,
                3.3641,3.4246,3.947,3.5965,3.3792};
            double[] latitudes  = new double[] {
                6.5744,6.4555,6.557,6.5273,
                6.5313,6.4619,6.6194,6.498,
                6.618,6.685,6.5392,6.4292,
                6.5059,6.6018,6.459,6.4316,
                6.4446,6.4549,6.6055,6.4963,
                6.5244};
            for (int i = 0; i < 21; i++)
            {
                LocationModel location = new LocationModel();
                
                location.Name = lgas[i];
                location.Latitude = latitudes[i];
                location.Longitude = longitudes[i];
                AddLocation(location);
            }

        }
        public void AddHospitals()
        {
            List<string> lgas = new List<string>()
            {
                "St. Nicholas Hospital, Lagos",
"Lagos University Teaching Hospital (LUTH)          ",
"First Consultant Hospital                          ",
"Creek Hospital                                     ",
"Lagos State University Teaching Hospital (LASUTH)  ",
"National Orthopaedic Hospital                      ",
"Lagoon Hospitals                                   ",
"Eko Hospital                                       ",
"Reddington Hospital                                ",
"Lagos Island General Hospital                      ",
"Yaba Psychiatric Hospital                          ",
"Flying Doctors Nigeria                             ",
"Mart Life Detox Clinic                             ",
"Gbagada General Hospital                           ",
"Ikeja General Hospital                             ",
"Ifako-Ijaiye General Hospital                      ",
"Isolo General Hospital                             ",
"Ikorodu General Hospital                           ",
            "Badagry General Hospital        "

            };
            int[] LocationIds = new int[] {
                16,4,19,16,11,1,19,11,19,
                16,12,11,2,2,11,21,9,8,14
            };
           
            for (int i = 0; i < 19; i++)
            {
                HospitalModel hospital = new HospitalModel();

                hospital.Name = lgas[i];
                hospital.LocationId = LocationIds[i];
                //AddHospital(hospital);
            }

        }


        public async void AddLocation(LocationModel locationModel)
        {
            LocationModel hospital = new LocationModel();

            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(locationModel), Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PostAsync(basUrl + "Location", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //HttpResponseMessage mssg = new HttpResponseMessage();
                        //mssg.StatusCode = System.Net.HttpStatusCode.OK;
                        hospital = JsonConvert.DeserializeObject<LocationModel>(apiResponse);
                    }
                }
            }
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
            //var newEnrolleeList = await AddEnrolleeDiseases(catList);

            return View("Index", catList);
        }

        public async Task<IActionResult> UpdateHospital(int id)
        {
            HospitalModel hospital = new HospitalModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.GetAsync(basUrl + "Hospital/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        hospital = JsonConvert.DeserializeObject<HospitalModel>(apiResponse);
                    }
                }
            }
            return View(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHospital(HospitalModel hospital)
        {
            HospitalModel anim = new HospitalModel();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(hospital), Encoding.UTF8, "application/json");
                    string url = basUrl + "Hospital/" + hospital.Id;
                    using (var response = await httpClient.PutAsync(url, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        anim = JsonConvert.DeserializeObject<HospitalModel>(apiResponse);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    using (var response = await httpClient.DeleteAsync(basUrl + "Hospital/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
