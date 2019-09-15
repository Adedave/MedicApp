using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicApp.Api.Domain.Models;
using MedicApp.Api.Domain.Services;
using MedicApp.Api.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly MedicAppDbContext _context;
        private readonly IHospitalService _hospitalService;
        private readonly IEnrolleeServiceAll _enrolleeService;


        public LocationController(MedicAppDbContext context, IHospitalService hospitalService,IEnrolleeServiceAll enrolleeServiceAll)
        {
            _context = context;
            _hospitalService = hospitalService;
            _enrolleeService = enrolleeServiceAll;
        }
        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<Location> Get()
        {
            return _context.Locations.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Location Get(int id)
        {
            return _context.Locations.Find(id);
        }

        [HttpGet("details")]
        public async Task<IEnumerable<LocationDetailsModel>> GetDetails(int lgaId, string startDate, string endDate)
        {
            List<Enrollee> maleEnrollees = new List<Enrollee>();
            List<Enrollee> femaleEnrollees = new List<Enrollee>();
            List<Location> locations = new List<Location>();
            List<LocationDetailsModel> locationDetailsModels = new List<LocationDetailsModel>();
            if (lgaId == 0)
            {
                locations = _context.Locations.ToList();
            }
            else
            {
                var location =  _context.Locations.Find(lgaId);
                locations.Add(location);
            }

            foreach (var item in locations)
            {
                int enrolleesCount = 0;
                LocationDetailsModel detailsModel = new LocationDetailsModel
                {
                    LgaId = item.Id,
                    LgaName = item.Name
                };
                var hospitals = (List<Hospital>)await _hospitalService.GetHospitalByLocationId(item.Id);
                    detailsModel.HospitalCount = hospitals.Count;
                foreach (var hospital in hospitals)
                {
                    enrolleesCount += hospital.Enrollees.Count;
                }
                    detailsModel.EnrolleesCount = enrolleesCount;
                maleEnrollees = (List<Enrollee>) await _enrolleeService.FindEnrolleeByGender("male", item.Id, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
                    detailsModel.MaleEnrolleesCount = maleEnrollees.Count;

                femaleEnrollees = (List<Enrollee>) await _enrolleeService.FindEnrolleeByGender("female", item.Id, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
                    detailsModel.FemaleEnrolleesCount = femaleEnrollees.Count;

                locationDetailsModels.Add(detailsModel);
            }


            return locationDetailsModels;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Location Location)
        {
            _context.Locations.Add(Location);
            _context.SaveChanges();
        }

        // PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]Location Location)
        //{
        //    _LocationService.UpdateLocation(Location);
        //   _context.SaveChanges();
        //}

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var location = _context.Locations.Find(id);
            _context.Locations.Remove(location);
            _context.SaveChanges();
        }
    }
}
