using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Service.Context;
using Project.Service.Models;

namespace Project.Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private IVehicleMakeService _vehicleMakeService;
        private VehicleManagementDbContext _dbContext;
        
        public VehicleModelService(IVehicleMakeService vehicleMakeService) 
        { 
            _vehicleMakeService = vehicleMakeService;
            _dbContext = new VehicleManagementDbContext();
        }

        public async Task<bool> CreateVehicleModel(VehicleModel vehicleModel)
        {
            VehicleMake vehicleMake = await _dbContext.VehicleMakes
                        .Include(m => m.VehicleModels)
                        .Where(m => m.Id == vehicleModel.VehicleMake.Id)
                        .FirstOrDefaultAsync();

            if (vehicleMake.VehicleModels == null)
            {
                vehicleMake.VehicleModels = new List<VehicleModel>();
            }

            vehicleMake.VehicleModels.Add(vehicleModel);
            int result = await _dbContext.SaveChangesAsync();
            return result == 1;
        }

        public async Task<bool> DeleteVehicleModel(int vehicleModelId)
        {
            VehicleModel vehicleModel = await GetVehicleModel(vehicleModelId);
            if (vehicleModel != null)
            {
                _dbContext.VehicleModels.Attach(vehicleModel);
                _dbContext.VehicleModels.Remove(vehicleModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditVehicleModel(int vehicleModelId, VehicleModel editedVehicleModel)
        {
            VehicleModel vehicleModel = await GetVehicleModel(vehicleModelId);
            if (vehicleModel != null)
            {
                vehicleModel.Name = editedVehicleModel.Name;
                vehicleModel.Abrv = editedVehicleModel.Abrv;
            }
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<VehicleModel>> GetAllVehicleModels()
        {
            List<VehicleModel> vehicleModels = await _dbContext.VehicleModels.Include(m => m.VehicleMake).ToListAsync();
            return await Task.FromResult(vehicleModels);
        }
        
        public async Task<VehicleModel> GetVehicleModel(int vehicleModelId)
        {
            VehicleModel vehicleModel = await _dbContext.VehicleModels
                                        .Where(m => m.Id == vehicleModelId)
                                        .Include(m => m.VehicleMake)
                                        .FirstOrDefaultAsync();

            return await Task.FromResult(vehicleModel);
        }
    }
}
