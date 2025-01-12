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
            // Arrange
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

            // Act
            var result = await vehicleMakeService.CreateVehicleMake(vehicleMake);

            Assert.True(result);

            var addedVehicleMake = await dbContext.VehicleMakes.FirstOrDefaultAsync();

            // Assert
            Assert.NotNull(addedVehicleMake);
            Assert.Equal("TestMakeName", addedVehicleMake.Name);
            Assert.Equal("TestMakeAbrv", addedVehicleMake.Abrv);
        }

        [Fact]
        public async Task GetVehicleMake_ReturnsCorrectVehicleMake()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var dbContext = new VehicleManagementDbContext(options);
            var vehicleMakeService = new VehicleMakeService(dbContext);

            var vehicleMake = new VehicleMake
            {
                Id = 1,
                Name = "TestMakeName",
                Abrv = "TestMakeAbrv"
            };

            await dbContext.VehicleMakes.AddAsync(vehicleMake);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await vehicleMakeService.GetVehicleMake(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicleMake.Id, result.Id);
            Assert.Equal(vehicleMake.Name, result.Name);
            Assert.Equal(vehicleMake.Abrv, result.Abrv);
        }

        [Fact]
        public async Task GetAllVehicleMakes_ReturnsAllVehicleMakes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var dbContext = new VehicleManagementDbContext(options);
            var vehicleMakeService = new VehicleMakeService(dbContext);

            var vehicleMake1 = new VehicleMake
            {
                Id = 1,
                Name = "Test1",
                Abrv = "Test1"
            };

            var vehicleMake2 = new VehicleMake
            {
                Id = 2,
                Name = "Test2",
                Abrv = "Test2"
            };

            await dbContext.VehicleMakes.AddAsync(vehicleMake1);
            await dbContext.VehicleMakes.AddAsync(vehicleMake2);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await vehicleMakeService.GetAllVehicleMakes();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.True(result[0].Name == vehicleMake1.Name);
        }

        [Fact]
        public async Task EditVehicleMake_ReturnTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var dbContext = new VehicleManagementDbContext(options);
            var vehicleMakeService = new VehicleMakeService(dbContext);

            VehicleMake originalVehicleMake = new VehicleMake
            { 
                Id = 1,
                Name = "Test1",
                Abrv = "Test1"
            };

            VehicleMake editedVehicleMake = new VehicleMake
            {
                Id = 1,
                Name = "Test1_edited",
                Abrv = "Test1_edited"
            };

            await dbContext.VehicleMakes.AddAsync(originalVehicleMake);
            await dbContext.SaveChangesAsync();


            // Act
            var result = vehicleMakeService.EditVehicleMake(1, editedVehicleMake);

            var returnedVehicleMake = await vehicleMakeService.GetVehicleMake(1);

            // Assert
            Assert.NotNull(result);
            Assert.True(returnedVehicleMake.Id == originalVehicleMake.Id);
            Assert.True(returnedVehicleMake.Id == editedVehicleMake.Id);
            Assert.True(returnedVehicleMake.Name == editedVehicleMake.Name);
            Assert.True(returnedVehicleMake.Abrv == editedVehicleMake.Abrv);
            Assert.True(returnedVehicleMake.Abrv == "Test1_edited");
        }

        [Fact]
        public async Task DeleteVehicleMake_ReturnTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<VehicleManagementDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var dbContext = new VehicleManagementDbContext(options);
            var vehicleMakeService = new VehicleMakeService(dbContext);

            VehicleMake vehicleMake = new VehicleMake
            {
                Id = 55,
                Name = "Test1_edited",
                Abrv = "Test1_edited"
            };

            await dbContext.VehicleMakes.AddAsync(vehicleMake);
            await dbContext.SaveChangesAsync();


            // Act
            var result = await vehicleMakeService.DeleteVehicleMake(55);
            var deletedVehicleMake = await vehicleMakeService.GetVehicleMake(55);

            // Assert
            Assert.True(result);
            Assert.Null(deletedVehicleMake);
        }
    }
}
