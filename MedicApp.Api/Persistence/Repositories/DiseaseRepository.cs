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
    public class DiseaseRepository:BaseRepository,IDiseaseRepository
    {
        public DiseaseRepository(MedicAppDbContext context):base(context)
        {
        }

        public void AddDisease(Disease disease)
        {
            _context.Diseases.Add(disease);
            _context.SaveChanges();
        }

        public async void DeleteDisease(int id)
        {
            Disease hospital = await _context.Diseases.FindAsync(id);
            _context.Diseases.Remove(hospital);
            _context.SaveChanges();
        }

        public Task<Disease> FindDiseaseByName(string diseaseName)
        {
            var disease = _context.Diseases.Where(x => x.DiseaseName == diseaseName).FirstOrDefaultAsync();
            return disease;
        }

        public async Task<IEnumerable<Disease>> GetEnrolleeDiseases(int enrolleeId)
        {
            var enrolleeDiseases = new List<Disease>();
            var enrolDiseases = await _context.EnrolleeDiseases.Where(x => x.EnrolleeId == enrolleeId).ToListAsync();
            foreach (var item in enrolDiseases)
            {
                var disease = await GetDiseaseById(item.DiseaseId);
                enrolleeDiseases.Add(disease);
            }

            return enrolleeDiseases;
        }

        public async Task<Disease> GetDiseaseById(int id)
        {
            return await _context.Diseases.FindAsync(id);
        }

        public async Task<IEnumerable<Disease>> ListAsync()
        {
            return await _context.Diseases.ToListAsync();
        }

        public async void UpdateDisease(Disease disease)
        {
            _context.Entry(disease).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
    }
}
