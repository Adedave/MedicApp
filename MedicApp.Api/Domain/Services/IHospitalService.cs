using MedicApp.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Api.Domain.Services
{
    public interface IHospitalService
    {
        Task<IEnumerable<Hospital>> ListAsync();
        Task<IEnumerable<Hospital>> GetHospitalByLocationId(int locationId);
        void AddHospital(Hospital hospital);
        Task<Hospital> GetHospitalById(int id);
        void UpdateHospital(Hospital hospital);
        Task<Hospital> FindHospitalByName(string hospitalName);
        void DeleteHospital(int id);
    }
}
