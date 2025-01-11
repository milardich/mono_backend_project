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
            //VehicleMake vehicleMake = await _vehicleMakeService.GetVehicleMake(vehicleModel.Id);
            //vehicleModel.VehicleMake = vehicleMake;
            //if (vehicleMake.VehicleModels == null)
            //{
            //    vehicleMake.VehicleModels = new List<VehicleModel>();
            //}

            //// WORKS:
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

        public Task<bool> DeleteVehicleModel(int vehicleMakeId, int vehicleModelId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditVehicleModel(int vehicleMakeId, int vehicleModelId, VehicleModel editedVehicleModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VehicleModel>> GetAllVehicleModels()
        {
            List<VehicleModel> vehicleModels = await _dbContext.VehicleModels.Include(m => m.VehicleMake).ToListAsync();
            return await Task.FromResult(vehicleModels);
        }
        
        public Task<VehicleModel> GetVehicleModel(int vehicleMakeId, int vehicleModelId)
        {
            throw new NotImplementedException();
        }
    }
}
