using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MedicApp.Api.Domain.Models;
using MedicApp.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class EnrolleeController : Controller
    {
        private readonly IEnrolleeServiceAll _enrolleeService;

        public EnrolleeController(IEnrolleeServiceAll enrolleeService)
        {
            _enrolleeService = enrolleeService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<Enrollee>> Get()
        {
            var ele = await _enrolleeService.ListAsync();
             //AssignDiseaseInternally(ele);
            return ele;
        }

        private void AssignDiseaseInternally(IEnumerable<Enrollee> ele)
        {
            int diseaseId = 3;
            foreach (var item in ele)
            {
                EnrolleeDisease enrolleeDisease = new EnrolleeDisease();
                if (diseaseId > 3)
                {
                    diseaseId = 1;
                }
                enrolleeDisease.DiseaseId = diseaseId;
                diseaseId++;
                enrolleeDisease.EnrolleeId = item.Id;
                GiveEnrolleeADisease(enrolleeDisease);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Task<Enrollee> Get(int id)
        {
            return _enrolleeService.GetEnrolleeById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Enrollee enrollee)
        {
            _enrolleeService.AddEnrollee(enrollee);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Enrollee enrollee)
        {
            _enrolleeService.UpdateEnrollee(enrollee);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _enrolleeService.DeleteEnrollee(id);
        }

        [HttpGet("age")]
        public async Task<IEnumerable<Enrollee>> FindEnrolleeByAge(int age,int lgaId, string startDate, string endDate)
        {
            var enrolleesByAge =  await _enrolleeService.FindEnrolleeByAge(age,lgaId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            return enrolleesByAge;
        }

        [HttpGet("ageRange")]
        public async Task<IEnumerable<Enrollee>> FindEnrolleeByAgeRange(int minAge, int maxAge, int lgaId, string startDate, string endDate)
        {
            var enrolleesByAgeRange = await _enrolleeService.FindEnrolleeByAgeRange(minAge,maxAge,lgaId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            return enrolleesByAgeRange;
        }

        [HttpGet("disease")]
        public async Task<IEnumerable<Enrollee>> FindEnrolleeByDisease(int diseaseId, int lgaId, string startDate, string endDate)
        {
             var enrolleeByDisease = await _enrolleeService.FindEnrolleeByDisease(diseaseId,lgaId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            return enrolleeByDisease;
        }

        [HttpPost("assign-disease")]
        public void GiveEnrolleeADisease([FromBody]EnrolleeDisease enrolleeDisease )
        {
            _enrolleeService.GiveEnrolleeADisease(enrolleeDisease.DiseaseId, enrolleeDisease.EnrolleeId);
        }


        [HttpGet("gender")]
        public Task<IEnumerable<Enrollee>> FindEnrolleeByGender(string gender, int lgaId, string startDate, string endDate)
        {
            var enrolleeByGender = _enrolleeService.FindEnrolleeByGender(gender, lgaId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            return enrolleeByGender;
        }
        
        [HttpGet("lga")]
        public Task<IEnumerable<Enrollee>> FindEnrolleeByLga(int lgaId, string startDate, string endDate)
        {
            var enrolleeByLga = _enrolleeService.FindEnrolleeByLga(lgaId, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            return enrolleeByLga;
        }

        //[HttpGet("location")]
        //public Task<IEnumerable<Enrollee>> FindEnrolleeByLocation(int location)
        //{
        //    return _enrolleeService.FindEnrolleeByLocation(location);
        //}


        //[HttpGet("name")]
        //public Task<Enrollee> FindEnrolleeByName(string enrolleeName)
        //{
        //    return _enrolleeService.FindEnrolleeByName(enrolleeName);
        //}
    }
}
