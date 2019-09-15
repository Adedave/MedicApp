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
    public class HospitalController : Controller
    {
        private readonly IHospitalService _hospitalService;
        //private readonly IMapper _mapper;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
            //_mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public Task<IEnumerable<Hospital>> Get()
        {
            return _hospitalService.ListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Task<Hospital> Get(int id)
        {
            return _hospitalService.GetHospitalById(id);
        }

        [HttpGet("hospitalByLocation")]
        public Task<IEnumerable<Hospital>> GetHospitalByLocationId(int locationId)
        {
            var hospitals =  _hospitalService.GetHospitalByLocationId(locationId);
            return hospitals;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Hospital hospital)
        {
            _hospitalService.AddHospital(hospital);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Hospital hospital)
        {
            _hospitalService.UpdateHospital(hospital);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _hospitalService.DeleteHospital(id);
        }
    }
}
