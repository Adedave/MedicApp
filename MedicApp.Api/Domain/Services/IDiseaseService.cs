using MedicApp.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Domain.Services
{
    public interface IDiseaseService
    {
        Task<IEnumerable<Disease>> ListAsync();
        void AddDisease(Disease disease);
        Task<Disease> GetDiseaseById(int id);
        void UpdateDisease(Disease disease);
        Task<Disease> FindDiseaseByName(string diseaseName);
        Task<IEnumerable<Disease>> GetEnrolleeDiseases(int enrolleeId);
        void DeleteDisease(int id);
    }
}
