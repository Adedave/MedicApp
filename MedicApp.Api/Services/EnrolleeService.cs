using MedicApp.Api.Domain.Models;
using MedicApp.Api.Domain.Repositories;
using MedicApp.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Services
{
    public class EnrolleeService : EnrolleeFindService, IEnrolleeServiceAll
    {
        private readonly IEnrolleeRepositoryAll _enrolleeRepositoryAll;

        public EnrolleeService(IEnrolleeRepositoryAll enrolleeRepositoryAll)
            :base(enrolleeRepositoryAll)
        {
            _enrolleeRepositoryAll = enrolleeRepositoryAll;
        }

        public void AddEnrollee(Enrollee enrollee)
        {
            _enrolleeRepositoryAll.AddEnrollee(enrollee);
        }

        public void DeleteEnrollee(int id)
        {
            _enrolleeRepositoryAll.DeleteEnrollee(id);
        }

        

        public Task<Enrollee> GetEnrolleeById(int id)
        {
            return _enrolleeRepositoryAll.GetEnrolleeById(id);
        }

        public async Task<IEnumerable<Enrollee>> ListAsync()
        {
            return await _enrolleeRepositoryAll.ListAsync();
        }

        public void UpdateEnrollee(Enrollee Enrollee)
        {
            _enrolleeRepositoryAll.UpdateEnrollee(Enrollee);
        }
    }
}
