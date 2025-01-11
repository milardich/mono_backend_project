﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Models;

namespace Project.Service.Services
{
    public interface IVehicleModelService
    {
        Task<bool> CreateVehicleModel(VehicleModel vehicleModel);
        Task<VehicleModel> GetVehicleModel(int vehicleMakeId, int vehicleModelId);
        Task<List<VehicleModel>> GetAllVehicleModels();
        Task<bool> EditVehicleModel(int vehicleMakeId, int vehicleModelId, VehicleModel editedVehicleModel);
        Task<bool> DeleteVehicleModel(int vehicleMakeId, int vehicleModelId);
    }
}
