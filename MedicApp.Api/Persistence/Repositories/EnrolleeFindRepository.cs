using MedicApp.Api.Domain.Models;
using MedicApp.Api.Domain.Repositories;
using MedicApp.Api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Persistence.Repositories
{
    public class EnrolleeFindRepository : BaseRepository , IEnrolleeFindRepository
    {
        private readonly IDiseaseRepository _diseaseRepository;
        
        public EnrolleeFindRepository(MedicAppDbContext context, IDiseaseRepository diseaseRepository) : base(context)
        {
            _diseaseRepository = diseaseRepository;
        }
        
        public async Task<IEnumerable<Enrollee>> FindEnrolleeByAge(int age, int locationId, DateTime startDate, DateTime endDate)
        {
            var e = await FindEnrolleeByLga(locationId, startDate, endDate);
            var response = e.Where(x => x.Age == age);
            return response;
        }

        public async Task<IEnumerable<Enrollee>> FindEnrolleeByAgeRange(int minAge, int maxAge, int locationId, DateTime startDate, DateTime endDate)
        {
            var e = await FindEnrolleeByLga(locationId, startDate, endDate);
            //check for when min age has a value and max age is zero
            return e.Where(x => x.Age >= minAge && x.Age <= (maxAge <= 0 ? 150 : maxAge));
        }

        public async Task<IEnumerable<Enrollee>> FindEnrolleeByDisease(int diseaseId, DateTime startDate, DateTime endDate)
        {
            var enrollees = new List<Enrollee>();
            var diseases = new List<EnrolleeDisease>();
           
            if (startDate.ToString() == "1/1/0001 12:00:00 AM" && endDate.ToString() != "1/1/0001 12:00:00 AM")
            {
                 diseases = await _context.EnrolleeDiseases.Include(x => x.Enrollee)
                    //same ternary operator can be used for the datetimes
                    .Where(x => x.DiseaseId == (diseaseId != 0 ? diseaseId : x.DiseaseId) && x.DateDiseaseDiagnosed <= endDate).ToListAsync();

            }
            else if (startDate.ToString() != "1/1/0001 12:00:00 AM" && endDate.ToString() == "1/1/0001 12:00:00 AM")
            {
                diseases = await _context.EnrolleeDiseases.Include(x => x.Enrollee).Where(x => x.DiseaseId == (diseaseId != 0 ? diseaseId : x.DiseaseId) && x.DateDiseaseDiagnosed >= startDate).ToListAsync();

            }
            else if (startDate.ToString() == "1/1/0001 12:00:00 AM" && endDate.ToString() == "1/1/0001 12:00:00 AM")
            {
                diseases = await _context.EnrolleeDiseases.Include(x => x.Enrollee).Where(x => x.DiseaseId == (diseaseId != 0 ? diseaseId : x.DiseaseId)).ToListAsync();
            }
            else
            {
                diseases = await _context.EnrolleeDiseases.Include(x => x.Enrollee).Where(x => x.DiseaseId == (diseaseId != 0 ? diseaseId : x.DiseaseId) && x.DateDiseaseDiagnosed >= startDate && x.DateDiseaseDiagnosed <= endDate).ToListAsync();
            }
            foreach (var item in diseases)
            {
                if (!enrollees.Contains(item.Enrollee))
                {
                    enrollees.Add(item.Enrollee);
                }
            }
            return enrollees;
        }

        public async Task<IEnumerable<Enrollee>> FindEnrolleeByGender(string gender, int locationId, DateTime startDate, DateTime endDate)
        {
            var e = await FindEnrolleeByLga(locationId,startDate,endDate);
            if (gender == "all")
            {
                return e;
            }
            var response =  e.Where(x => x.Gender.Equals(gender, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return response;
        }



        public async Task<IEnumerable<Enrollee>> FindEnrolleeByLga(int lgaId, DateTime startDate, DateTime endDate)
        {
            List<Enrollee> enrollees = new List<Enrollee>();
            List<Hospital> hospitals = new List<Hospital>();
            if (lgaId == 0)
            {
                 hospitals = await _context.Hospitals.Include(x => x.Enrollees).Include(h => h.Location).ToListAsync();

            }
            else
            {
                 hospitals = await _context.Hospitals.Include(x => x.Enrollees).Include(h => h.Location).Where(x => x.LocationId == lgaId).ToListAsync();
            }
            foreach (var item in hospitals)
            {
                enrollees.AddRange(item.Enrollees);
            }
             //extract the below logic in a method called Compare enrollees with time
            List<Enrollee> finalList = new List<Enrollee>();
            var timedEnrollees = await FindEnrolleeByDisease(0,startDate,endDate);
            foreach (var item in enrollees)
            {
                if (timedEnrollees.Contains(item))
                {
                    finalList.Add(item);
                }
            }
            foreach (var item in finalList)
            {
                item.Lga = item.Hospital.Location.Name;
                item.Hospital = null;
                item.Diseases = null;
            }
            return finalList;
        }
        

        public async Task<Enrollee> FindEnrolleeByName(string enrolleeName)
        {
            Enrollee enrollee = await _context.Enrollees.Where(x => x.Name.Equals(enrolleeName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();
            return enrollee;
        }

        public void GiveEnrolleeADisease(int diseaseId, int enrolleeId)
        {
            try
            {
                var enrolleeDisease = new EnrolleeDisease()
                {
                    EnrolleeId = enrolleeId,
                    DiseaseId = diseaseId,
                    DateDiseaseDiagnosed = DateTime.Now
                };
                _context.Add(enrolleeDisease);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                
            }
            
        }
    }
}
