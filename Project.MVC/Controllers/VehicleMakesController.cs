using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;
using Project.Service.Models;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly IVehicleMakeService _vehicleMakeService;

        public VehicleMakesController(IVehicleMakeService vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleMakeViewModel viewModel)
        {
            VehicleMake vehicleMake = new VehicleMake
            {
                Name = viewModel.Name,
                Abrv = viewModel.Abrv
            };

            bool result = await _vehicleMakeService.CreateVehicleMake(vehicleMake);
            return RedirectToAction("List", "VehicleMakes");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<VehicleMake> vehicleMakes = await _vehicleMakeService.GetAllVehicleMakes();
            List<VehicleMakeViewModel> viewModels = new List<VehicleMakeViewModel>();
            foreach (var vehicleMake in vehicleMakes)
            {
                viewModels.Add(
                    new VehicleMakeViewModel
                    {
                        Id = vehicleMake.Id,
                        Name = vehicleMake.Name,
                        Abrv = vehicleMake.Abrv
                    }
                );
            }
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            VehicleMake vehicleMake = await _vehicleMakeService.GetVehicleMake(id);
            VehicleMakeViewModel viewModel = new VehicleMakeViewModel
            {
                Id = vehicleMake.Id,
                Name = vehicleMake.Name,
                Abrv = vehicleMake.Abrv
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VehicleMakeViewModel viewModel)
        {
            VehicleMake editedVehicleMake = new VehicleMake
            {
                Id = id,
                Name = viewModel.Name,
                Abrv = viewModel.Abrv
            };
            bool result = await _vehicleMakeService.EditVehicleMake(id, editedVehicleMake);
            if (result)
            {
                return RedirectToAction("List", "VehicleMakes");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleMakeService.DeleteVehicleMake(id);
            return RedirectToAction("List", "VehicleMakes");
        }
    }
}
