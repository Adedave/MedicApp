using MedicApp.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Domain.Services
{
    public interface IEnrolleeFindService
    {
        Task<Enrollee> FindEnrolleeByName(string enrolleeName);
        Task<IEnumerable<Enrollee>> FindEnrolleeByAge(int age, int locationId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Enrollee>> FindEnrolleeByAgeRange(int minAge, int maxAge, int locationId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Enrollee>> FindEnrolleeByGender(string gender, int locationId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Enrollee>> FindEnrolleeByLga(int lgaId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<Enrollee>> FindEnrolleeByDisease(int diseaseId, int locationId, DateTime startDate, DateTime endDate);
        void GiveEnrolleeADisease(int diseaseId , int enrolleeId);
    }
}
