using Project.Service.Models;

namespace Project.MVC.Models
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Abrv { get; set; }
        public VehicleMake VehicleMake { get; set; }
    }
}
