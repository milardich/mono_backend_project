using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Service.Models;

namespace Project.Service.Context
{
    public class VehicleManagementDbContext : DbContext
    {
        public VehicleManagementDbContext(DbContextOptions<VehicleManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
}
