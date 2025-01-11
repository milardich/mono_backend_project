﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
    public class VehicleMake
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Abrv { get; set; }
        public List<VehicleModel> VehicleModels { get; set; }
    }
}
