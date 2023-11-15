using Microsoft.AspNetCore.Mvc;
using api.Models.Vehicle;
using api.Services;
using api.Models.Dealership; // can remove

namespace api.Controllers;

[Route("api/vehicle")]
[ApiController]

public class VehicleController : ControllerBase
{

    public static VehicleServices services = new VehicleServices();


    [HttpPost("create-car")]
    public IActionResult CreateVehicle(VehicleCreate vehicleCreate)
    {

        bool createdVehicle = services.CreateVehicle(vehicleCreate);

        if (createdVehicle is true)
        {
            return Ok($"Vehicle with the make {vehicleCreate.Make} and model {vehicleCreate.Model} has been added to dealership id {vehicleCreate.DealershipId}");

        }
        else
        {
            return BadRequest("No Vehicle was Created");
        }


    }

        [HttpPost("remove-car")]
    public IActionResult RemoveVehicle(VehicleRemove vehicleRemove)
    {

        bool removedVehicle = services.RemoveVehicle(vehicleRemove);

        if (removedVehicle is true)
        {
            return Ok($"Vehicle with the make {vehicleRemove.Make} and model {vehicleRemove.Model} has been removed from dealership id {vehicleRemove.DealershipId}");

        }
        else
        {
            return BadRequest("Vehicle does not exist");
        }


    }

    [HttpPost("update-stock")]
    public IActionResult UpdateStock(VehicleStock vehicleStock)
    {

        bool stockVehicle = services.StockVehicle(vehicleStock);

        if (stockVehicle is true)
        {
            return Ok($"Vehicle with the make {vehicleStock.Make} and model {vehicleStock.Model} stock has changed to {vehicleStock.Stock}");

        }
        else
        {
            return BadRequest("The vehicle does not exist");
        }


    }

}
