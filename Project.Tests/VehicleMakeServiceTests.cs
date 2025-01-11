using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Project.Service.Context;
using Project.Service.Models;
using Project.Service.Services;

namespace Project.Tests
{
    public class VehicleMakeServiceTests
    {
        [Fact]
        public async Task CreateVehicleMake_ReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<VehicleManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "defaultDb")
                .Options;

            var dbContext = new VehicleManagementDbContext(options);
            var vehicleMakeService = new VehicleMakeService(dbContext);
            
            var vehicleMake = new VehicleMake
            {
                Name = "TestMakeName",
                Abrv = "TestMakeAbrv"
            };

            var result = await vehicleMakeService.CreateVehicleMake(vehicleMake);

            Assert.True(result);

            var addedVehicleMake = await dbContext.VehicleMakes.FirstOrDefaultAsync();

            Assert.NotNull(addedVehicleMake);
            Assert.Equal("TestMakeName", addedVehicleMake.Name);
            Assert.Equal("TestMakeAbrv", addedVehicleMake.Abrv);
        }

        [Fact]
        public async Task GetVehicleMake_ReturnsVehicleMake()
        {

        }


    }
}
