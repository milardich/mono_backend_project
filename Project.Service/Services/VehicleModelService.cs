using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Service.Context;
using Project.Service.Models;

namespace Project.Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleManagementDbContext _dbContext;
        
        public VehicleModelService(VehicleManagementDbContext dbContext) 
        { 
            _dbContext = dbContext;
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

        public async Task<List<VehicleModel>> GetFilteredVehicleModels(string sortOrder, string stringSearch)
        {
            IQueryable<VehicleModel> query = _dbContext.VehicleModels
                .Where(m => string.IsNullOrEmpty(stringSearch) ||
                            m.VehicleMake.Name.Contains(stringSearch) ||
                            m.VehicleMake.Abrv.Contains(stringSearch) ||
                            m.Name.Contains(stringSearch))
                .Include(m => m.VehicleMake)
                .AsNoTracking();

            query = sortOrder switch
            {
                "name_asc" => query.OrderBy(m => m.Name),
                "name_desc" => query.OrderByDescending(m => m.Name),
                "abrv_asc" => query.OrderBy(m => m.Abrv),
                "abrv_desc" => query.OrderByDescending(m => m.Abrv),
                _ => query.OrderBy(m => m.Name),
            };

            return await query.ToListAsync();
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
