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
    public class DiseaseController : Controller
    {
        private readonly IDiseaseService _diseaseService;
        //private readonly IMapper _mapper;

        public DiseaseController(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
            //_mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        public Task<IEnumerable<Disease>> Get()
        {
            return _diseaseService.ListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Task<Disease> Get(int id)
        {
            return _diseaseService.GetDiseaseById(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Disease Disease)
        {
            _diseaseService.AddDisease(Disease);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Disease Disease)
        {
            _diseaseService.UpdateDisease(Disease);
        }

        [HttpGet("DiseasesOfEnrollee")]
        public Task<IEnumerable<Disease>> FindDiseasesOfEnrollee(int enrolleeId)
        {
            return _diseaseService.GetEnrolleeDiseases(enrolleeId);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _diseaseService.DeleteDisease(id);
        }
    }
}
