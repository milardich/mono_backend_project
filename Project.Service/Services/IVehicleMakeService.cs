using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Dtos;
using Project.Service.Models;

namespace Project.Service.Services
{
    public interface IVehicleMakeService
    {
        Task<bool> CreateVehicleMake(VehicleMakeDTO vehicleMakeDTO);
        Task<VehicleMakeDTO> GetVehicleMake(int id);
        Task<List<VehicleMakeDTO>> GetAllVehicleMakes();
    }
}
