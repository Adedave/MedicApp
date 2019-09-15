using MedicApp.Api.Domain.Models;
using MedicApp.Api.Domain.Repositories;
using MedicApp.Api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Persistence.Repositories
{
    public class HospitalRepository:BaseRepository, IHospitalRepository
    {
        public HospitalRepository(MedicAppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Hospital>> ListAsync()
        {
            return await _context.Hospitals.ToListAsync();
        }

        public async Task<Hospital> GetHospitalById(int id)
        {
            return await _context.Hospitals.FindAsync(id);
        }
        
        public void AddHospital(Hospital hospital)
        {
            _context.Hospitals.Add(hospital);
             _context.SaveChanges();
        }

        public async void UpdateHospital(Hospital hospital)
        {
            _context.Entry(hospital).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Hospital> FindHospitalByName(string hospitalName)
        {
           Hospital hospital = await _context.Hospitals.Where(x => x.Name == hospitalName).FirstOrDefaultAsync();
            return hospital;
        }

        public async void DeleteHospital(int id)
        {
            Hospital hospital = await _context.Hospitals.FindAsync(id);
            _context.Hospitals.Remove(hospital);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Hospital>> GetHospitalByLocationId(int locationId)
        {
            var hospitals = await _context.Hospitals.Include(x => x.Enrollees).Where(x => x.LocationId == locationId).ToListAsync();
            return hospitals;
        }
    }
}
