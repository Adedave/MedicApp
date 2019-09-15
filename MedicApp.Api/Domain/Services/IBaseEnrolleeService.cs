using MedicApp.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Domain.Services
{
    public interface IBaseEnrolleeService
    {
        Task<IEnumerable<Enrollee>> ListAsync();
        void AddEnrollee(Enrollee enrollee);
        Task<Enrollee> GetEnrolleeById(int id);
        void UpdateEnrollee(Enrollee enrollee);
        void DeleteEnrollee(int id);
    }
}
