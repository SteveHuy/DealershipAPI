using api.Models.Dealership;
using api.Models.Vehicle;

using api.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.Services
{
    public class VehicleServices
    {


        public bool CreateVehicle(VehicleCreate vehicleCreate)
        {
            var dealershipId = vehicleCreate.DealershipId;



            if (!DealershipServices.dealerships.ContainsKey(dealershipId))
            {
                return false;
            }

            var dealership = DealershipServices.dealerships[dealershipId];
            var dealershipVehicles = dealership.Vehicles;

            var make = vehicleCreate.Make;
            var model = vehicleCreate.Model;
            var modelYear = vehicleCreate.ModelYear;

            // Check if the vehicle already exists in the dealership
            if (dealershipVehicles.TryGetValue(make, out var existingVehicles))
            {
                if (existingVehicles.Any(vehicle => vehicle.Model == model && vehicle.ModelYear == modelYear))
                {
                    return false; // Vehicle already exists
                }
            }
            else
            {
                existingVehicles = new List<Vehicle>();
                dealershipVehicles[make] = existingVehicles;
            }

            var stock = vehicleCreate.Stock;
            var newVehicle = new Vehicle(make, model, modelYear, dealershipId, dealership, stock);
            existingVehicles.Add(newVehicle);
            return true; // Vehicle created and added
        }

        public bool RemoveVehicle(VehicleRemove vehicleRemove)
        {
            var Make = vehicleRemove.Make;
            var Model = vehicleRemove.Model;
            var ModelYear = vehicleRemove.ModelYear;
            var DealershipId = vehicleRemove.DealershipId;

            if (DealershipServices.dealerships.TryGetValue(DealershipId, out var dealership))
            {
                if (dealership.Vehicles.TryGetValue(Make, out var vehicleMakeList))
                {
                    for (int i = 0; i < vehicleMakeList.Count; i++)
                    {
                        var vehicle = vehicleMakeList[i];
                        if (vehicle.Model == Model && vehicle.ModelYear == ModelYear)
                        {
                            vehicleMakeList.RemoveAt(i);
                            return true; // Vehicle found and either updated or removed
                        }
                    }
                }
            }

            return false; // Vehicle not found
        }





        public bool StockVehicle(VehicleStock vehicleStock)
        {
            var Make = vehicleStock.Make;
            var Model = vehicleStock.Model;
            var ModelYear = vehicleStock.ModelYear;
            var DealershipId = vehicleStock.DealershipId;

            bool vehicleFound = false;

            if (DealershipServices.dealerships.TryGetValue(DealershipId, out var dealership))
            {
                if (dealership.Vehicles.TryGetValue(Make, out var vehicleMakeList))
                {
                    foreach (var vehicle in vehicleMakeList)
                    {
                        if (vehicle.Model == Model && vehicle.ModelYear == ModelYear)
                        {
                            vehicle.Stock = vehicleStock.Stock;
                            vehicleFound = true;
                        }
                    }
                }
            }

            return vehicleFound;
        }





    }
}