using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;
using Project.Service.Dtos;
using Project.Service.Services;

namespace Project.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IVehicleMakeService _vehicleMakeService;

    public HomeController(ILogger<HomeController> logger, IVehicleMakeService vehicleMakeService)
    {
        _logger = logger;
        _vehicleMakeService = vehicleMakeService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        return View();
    }

    public async Task<IActionResult> VehicleMake()
    {
        List<VehicleMakeDTO> makeDTOs = await _vehicleMakeService.GetAllVehicleMakes();
        VehicleMakeViewModel viewModel = new VehicleMakeViewModel();
        viewModel.makeDTOs = new List<VehicleMakeDTO>();
        viewModel.makeDTOs.AddRange(makeDTOs);
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
        });
    }
}
