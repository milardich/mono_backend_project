using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Service.Context;
using Project.Service.Dtos;
using Project.Service.Models;

namespace Project.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleManagementDbContext _dbContext;

        public VehicleMakeService()
        {
            _dbContext = new VehicleManagementDbContext();
        }

        public async Task<bool> CreateVehicleMake(VehicleMakeDTO vehicleMakeDTO)
        {
            VehicleMake vehicleMake = new VehicleMake 
            { 
                Name = vehicleMakeDTO.Name,
                Abrv = vehicleMakeDTO.Abrv 
            };

            _dbContext.VehicleMakes.Add(vehicleMake);
            int result = await _dbContext.SaveChangesAsync();
            return result == 1;
        }

        public async Task<VehicleMakeDTO> GetVehicleMake(int id)
        {
            VehicleMake makeModel = await _dbContext.FindAsync<VehicleMake>(id);
            VehicleMakeDTO makeDTO = new VehicleMakeDTO
            {
                Name = makeModel.Name,
                Abrv = makeModel.Abrv
            };
            return await Task.FromResult(makeDTO);
        }

        public async Task<List<VehicleMakeDTO>> GetAllVehicleMakes()
        {
            var makes = await _dbContext.VehicleMakes.ToListAsync();
            List<VehicleMakeDTO> makeDTOs = new List<VehicleMakeDTO>();
            foreach (var make in makes)
            {
                VehicleMakeDTO makeDTO = new VehicleMakeDTO 
                { 
                    Abrv = make.Abrv, 
                    Name = make.Name 
                };
                makeDTOs.Add(makeDTO);
            }
            return await Task.FromResult(makeDTOs);
        }
    }
}
