using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;
using Project.Service.Models;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class VehicleModelsController : Controller
    {
        private IVehicleModelService _vehicleModelService;

        public VehicleModelsController(IVehicleModelService vehicleModelService) 
        {
            _vehicleModelService = vehicleModelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<VehicleModelViewModel> viewModels = new List<VehicleModelViewModel>();
            List<VehicleModel> vehicleModels = await _vehicleModelService.GetAllVehicleModels();
            foreach (var vehicleModel in vehicleModels)
            {
                viewModels.Add(
                    new VehicleModelViewModel
                    {
                        Id = vehicleModel.Id,
                        Name = vehicleModel.Name,
                        Abrv = vehicleModel.Abrv,
                        VehicleMake = vehicleModel.VehicleMake,
                    }
                );
            }
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleModelViewModel viewModel)
        {
            VehicleModel vehicleModel = new VehicleModel
            {
                Name = viewModel.Name,
                Abrv = viewModel.Abrv,
                VehicleMake = viewModel.VehicleMake
            };

            bool result = await _vehicleModelService.CreateVehicleModel(vehicleModel);

            return RedirectToAction("List", "VehicleModels");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            VehicleModel vehicleModel = await _vehicleModelService.GetVehicleModel(id);
            VehicleModelViewModel viewModel = new VehicleModelViewModel
            {
                Id = vehicleModel.Id,
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                VehicleMake = vehicleModel.VehicleMake
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VehicleModelViewModel viewModel)
        {
            VehicleModel editedVehicleModel = new VehicleModel
            {
                Id = id,
                Name = viewModel.Name,
                Abrv = viewModel.Abrv,
                VehicleMake = viewModel.VehicleMake
            };
            bool result = await _vehicleModelService.EditVehicleModel(id, editedVehicleModel);
            if (result)
            {
                return RedirectToAction("List", "VehicleModels");
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleModelService.DeleteVehicleModel(id);
            return RedirectToAction("List", "VehicleModels");
        }
    }
}
