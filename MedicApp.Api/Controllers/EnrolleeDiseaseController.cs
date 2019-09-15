using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicApp.Api.Domain.Models;
using MedicApp.Api.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class EnrolleeDiseaseController : Controller
    {
        private readonly MedicAppDbContext _context;

        public EnrolleeDiseaseController(MedicAppDbContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<EnrolleeDisease> Get()
        {
            var ele = _context.EnrolleeDiseases.ToList();
           
            return ele;
        }

        private void ChangeEnrolleeDiseaseData(EnrolleeDisease item)
        {
            item.DateDiseaseDiagnosed = RandomDay();
            //logic for randonmize time
            Put(item);
        }
        DateTime RandomDay()
        {
            Random random = new Random();
            DateTime start = new DateTime(2018, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range)).AddHours(random.Next(0, 24)).AddMinutes(random.Next(0, 60)).AddSeconds(random.Next(0, 60)); ;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public EnrolleeDisease Get(int id)
        {
            var ele = _context.EnrolleeDiseases.Find(id);
            return ele;
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]EnrolleeDisease enrolleeDisease)
        {
            _context.EnrolleeDiseases.Add(enrolleeDisease);
            _context.SaveChanges();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put([FromBody]EnrolleeDisease enrolleeDisease)
        {
            if (enrolleeDisease != null)
            {
                _context.Entry(enrolleeDisease).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var location = _context.EnrolleeDiseases.Find(id);
            _context.EnrolleeDiseases.Remove(location);
            _context.SaveChanges();
        }
    }
}
