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
    public class EnrolleeRepository:EnrolleeFindRepository, IEnrolleeRepositoryAll
    {
        public EnrolleeRepository(MedicAppDbContext context,IDiseaseRepository diseaseRepository) : base(context, diseaseRepository)
        {
        }
        
        public void AddEnrollee(Enrollee enrollee)
        {
            _context.Enrollees.Add(enrollee);
            _context.SaveChanges();
        }

        public async void DeleteEnrollee(int id)
        {
            Enrollee enrollee = await _context.Enrollees.FindAsync(id);
            _context.Enrollees.Remove(enrollee);
            _context.SaveChanges();
        }
        public async Task<Enrollee> GetEnrolleeById(int id)
        {
            return await _context.Enrollees.FindAsync(id);
        }
        

        public async Task<IEnumerable<Enrollee>> ListAsync()
        {
            return await _context.Enrollees.ToListAsync();
        }

        public void UpdateEnrollee(Enrollee enrollee)
        {
            _context.Entry(enrollee).State = EntityState.Modified;
             _context.SaveChanges();
        }
    }
}
