using MedicApp.Api.Domain.Models;
using MedicApp.Api.Domain.Repositories;
using MedicApp.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Services
{
    public class EnrolleeFindService:IEnrolleeFindService
    {
        private readonly IEnrolleeRepositoryAll _enrolleeRepository;

        public EnrolleeFindService(IEnrolleeRepositoryAll enrolleeRepository)
        {
            _enrolleeRepository = enrolleeRepository;
        }

        public async Task<IEnumerable<Enrollee>> FindEnrolleeByAge(int age, int locationId, DateTime startDate, DateTime endDate)
        {
            return await _enrolleeRepository.FindEnrolleeByAge(age, locationId, startDate, endDate);
        }

        public async Task<IEnumerable<Enrollee>> FindEnrolleeByAgeRange(int minAge, int maxAge, int locationId, DateTime startDate, DateTime endDate)
        {
            return await _enrolleeRepository.FindEnrolleeByAgeRange(minAge,maxAge, locationId, startDate, endDate);
        }

        public async Task<IEnumerable<Enrollee>> FindEnrolleeByDisease(int diseaseId, int locationId, DateTime startDate, DateTime endDate)
        {
            var enrollees = await _enrolleeRepository.FindEnrolleeByDisease(diseaseId, startDate, endDate);

            var lgaEnrollees = await _enrolleeRepository.FindEnrolleeByLga(locationId, startDate, endDate);
            var finalEnrolleeList = new List<Enrollee>();
            foreach (var item in enrollees)
            {
                if (lgaEnrollees.Contains(item))
                {
                    finalEnrolleeList.Add(item);
                }
            }
            return finalEnrolleeList;
        }

        public Task<IEnumerable<Enrollee>> FindEnrolleeByGender(string gender, int locationId, DateTime startDate, DateTime endDate)
        {
            return _enrolleeRepository.FindEnrolleeByGender(gender, locationId,startDate,endDate);
        }

        public Task<IEnumerable<Enrollee>> FindEnrolleeByLga(int lgaId, DateTime startDate, DateTime endDate)
        {
            return _enrolleeRepository.FindEnrolleeByLga(lgaId, startDate, endDate);
        }
        
        public Task<Enrollee> FindEnrolleeByName(string enrolleeName)
        {
            return _enrolleeRepository.FindEnrolleeByName(enrolleeName);
        }

        public void GiveEnrolleeADisease(int diseaseId, int enrolleeId)
        {
            _enrolleeRepository.GiveEnrolleeADisease(diseaseId, enrolleeId);
        }
    }
}
