using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Models;

namespace Project.Service.Services
{
    public interface IVehicleMakeService
    {
        Task<bool> CreateVehicleMake(VehicleMake vehicleMake);
        Task<VehicleMake> GetVehicleMake(int id);
        Task<List<VehicleMake>> GetAllVehicleMakes();
        Task<bool> EditVehicleMake(int id, VehicleMake editedVehicleMake);
        Task<bool> DeleteVehicleMake(int id);
    }
}
