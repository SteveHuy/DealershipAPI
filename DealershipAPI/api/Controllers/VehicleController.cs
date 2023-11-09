using Microsoft.AspNetCore.Mvc;
using api.Models.Vehicle;
using api.Data;
using api.Services;
using System; // can remove

namespace api.Controllers;

[Route("api/vehicle")]
[ApiController]

public class VehicleController : ControllerBase
{



    [HttpPost("create-car")]
    public IActionResult CreateVehicle(VehicleCreate vehicleCreate)
    {

        var dealershipId = vehicleCreate.DealershipId;
        var dealershipService = DealershipController.services;

        var dealershipDictionary = DealershipServices.dealerships;

        // check if the that dealership ID exist

        if (!dealershipDictionary.ContainsKey(dealershipId))
        { // if it does not contain the ID
            return BadRequest($"There is no dealership with the ID {dealershipId}.");
        }

        var dealership = dealershipDictionary[dealershipId];

        var dealershipVehicles = dealership.Vehicles; // dictionary of dealership vehicles

        // lets create the new vehicle 
        var make = vehicleCreate.Make;
        var model = vehicleCreate.Model;
        var modelYear = vehicleCreate.ModelYear;
        var stock = vehicleCreate.Stock;
        Vehicle newVehicle = new Vehicle(make, model, modelYear, dealershipId, dealership, stock);

        // if there is already a list of that Maker within the dictionary
        if (dealershipVehicles.ContainsKey(vehicleCreate.Make))
        {
            var dealershipVehiclesMake = dealershipVehicles[vehicleCreate.Make];

            // see if it matches with any other vehicle with the same make
            foreach (Vehicle vehicle in dealershipVehiclesMake)
            {

                // new vehicle 
                var newVehicleModel = vehicleCreate.Model;
                var newVehicleModelYear = vehicleCreate.ModelYear;

                // already existing vehicle
                var existingVehicleModel = vehicle.Model;
                var existingVehicleModelYear = vehicle.ModelYear;

                if (newVehicleModel == existingVehicleModel && newVehicleModelYear == existingVehicleModelYear)
                {
                    return BadRequest("This vehicle already exist within the dealership");
                }


            }
            // it did not match any other cars there add the new vehicle
            dealershipVehicles[vehicleCreate.Make].Add(newVehicle);
        }
        else
        {
            // there is no existing list of the maker make a new list and make the new car
            List<Vehicle> newMakeList = new List<Vehicle>
            {
                newVehicle
            };
            dealershipVehicles.Add(vehicleCreate.Make, newMakeList);
        }


        return Ok($"Vehicle with the make {make} and model {model} has been added to dealership id {dealershipId}");

    }

}
