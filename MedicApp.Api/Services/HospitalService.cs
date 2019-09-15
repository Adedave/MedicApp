using MedicApp.Api.Domain.Models;
using MedicApp.Api.Domain.Repositories;
using MedicApp.Api.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicApp.Api.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;

        public HospitalService(IHospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }

        public void AddHospital(Hospital hospital)
        {
            _hospitalRepository.AddHospital(hospital);
        }

        public void DeleteHospital(int id)
        {
            _hospitalRepository.DeleteHospital(id);
        }

        public Task<Hospital> FindHospitalByName(string hospitalName)
        {
            return _hospitalRepository.FindHospitalByName(hospitalName);
        }

        public Task<Hospital> GetHospitalById(int id)
        {
            return _hospitalRepository.GetHospitalById(id);
        }

        public Task<IEnumerable<Hospital>> GetHospitalByLocationId(int locationId)
        {
            return _hospitalRepository.GetHospitalByLocationId(locationId);
        }

        public async Task<IEnumerable<Hospital>> ListAsync()
        {
            return await _hospitalRepository.ListAsync();
        }

        public void UpdateHospital(Hospital hospital)
        {
            _hospitalRepository.UpdateHospital(hospital);
        }
    }
}
