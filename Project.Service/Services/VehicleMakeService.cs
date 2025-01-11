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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleManagementDbContext _dbContext;

        public VehicleMakeService(VehicleManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateVehicleMake(VehicleMake vehicleMake)
        {
            await _dbContext.VehicleMakes.AddAsync(vehicleMake);
            int result = await _dbContext.SaveChangesAsync();
            return result == 1;
        }

        public async Task<VehicleMake> GetVehicleMake(int id)
        {
            VehicleMake vehicleMake = await _dbContext.VehicleMakes
                .FirstOrDefaultAsync(x => x.Id == id);
            return await Task.FromResult(vehicleMake);
        }

        public async Task<List<VehicleMake>> GetAllVehicleMakes()
        {
            var vehicleMakes = await _dbContext.VehicleMakes.ToListAsync();
            return await Task.FromResult(vehicleMakes);
        }

        public async Task<bool> EditVehicleMake(int id, VehicleMake editedVehicleMake)
        {
            VehicleMake vehicleMake = await GetVehicleMake(id);
            if (vehicleMake != null)
            {
                vehicleMake.Name = editedVehicleMake.Name;
                vehicleMake.Abrv = editedVehicleMake.Abrv;
            }
            int result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteVehicleMake(int id)
        {
            VehicleMake vehicleMake = await GetVehicleMake(id);
            if (vehicleMake != null)
            {
                _dbContext.VehicleMakes.Attach(vehicleMake);
                _dbContext.VehicleMakes.Remove(vehicleMake);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //public async Task<VehicleMake> GetVehicleMake(string name)
        //{
        //    var vehicleMake = _dbContext.VehicleMakes
        //                                .Where(m => m.Name == name)
        //                                .FirstOrDefault();
        //    throw new NotImplementedException();
        //}
    }
}
