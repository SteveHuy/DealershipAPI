using api.Models.Dealership;
using api.Models.Vehicle;
using api.Data;


namespace api.Services
{
    /// <summary>
    /// Service class for managing vehicles and their operations.
    /// </summary>
    public class VehicleServices
    {
        /// <summary>
        /// Creates a new vehicle and adds it to the specified dealership's inventory.
        /// </summary>
        /// <param name="vehicleCreate">Information for creating the new vehicle.</param>
        /// <returns>True if the vehicle is created and added successfully, false otherwise.</returns>
        public bool CreateVehicle(VehicleCreate vehicleCreate)
        {
            var dealershipId = vehicleCreate.DealershipId;

            // Check if the dealership exists
            if (!DealershipServices.dealerships.ContainsKey(dealershipId))
            {
                return false; // Dealership does not exist
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

        /// <summary>
        /// Removes a vehicle from the specified dealership's inventory based on make, model, and model year.
        /// </summary>
        /// <param name="vehicleRemove">Information for removing the vehicle.</param>
        /// <returns>True if the vehicle is found and removed successfully, false otherwise.</returns>
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
                            return true; // Vehicle found and removed
                        }
                    }
                }
            }

            return false; // Vehicle not found
        }

        /// <summary>
        /// Updates the stock level of a vehicle in the specified dealership's inventory.
        /// </summary>
        /// <param name="vehicleStock">Information for updating the vehicle stock level.</param>
        /// <returns>True if the vehicle is found and the stock level is updated successfully, false otherwise.</returns>
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
