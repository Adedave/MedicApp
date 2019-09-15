using MedicApp.Api.Domain.Models;
using MedicApp.Api.Domain.Repositories;
using MedicApp.Api.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicApp.Api.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepository _diseaseRepository;

        public DiseaseService(IDiseaseRepository diseaseRepository)
        {
            _diseaseRepository = diseaseRepository;
        }
        public void AddDisease(Disease disease)
        {
            _diseaseRepository.AddDisease(disease);
        }

        public void DeleteDisease(int id)
        {
            _diseaseRepository.DeleteDisease(id);
        }

        public Task<Disease> FindDiseaseByName(string diseaseName)
        {
            return _diseaseRepository.FindDiseaseByName(diseaseName);
        }

        public Task<Disease> GetDiseaseById(int id)
        {
            return _diseaseRepository.GetDiseaseById(id);
        }

        public Task<IEnumerable<Disease>> ListAsync()
        {
            return _diseaseRepository.ListAsync();
        }

        public async Task<IEnumerable<Disease>> GetEnrolleeDiseases(int enrolleeId)
        {
            return await _diseaseRepository.GetEnrolleeDiseases(enrolleeId);
        }
        public void UpdateDisease(Disease disease)
        {
            _diseaseRepository.UpdateDisease(disease);
        }
    }
}
