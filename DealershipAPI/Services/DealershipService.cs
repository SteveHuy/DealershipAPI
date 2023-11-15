using api.Models.Dealership;
using api.Data;


namespace api.Services
{
    /// <summary>
    /// Service class for managing dealerships and their operations.
    /// </summary>
    public class DealershipServices
    {
        /// <summary>
        /// Dictionary to store dealerships with their IDs as keys.
        /// </summary>
        public static Dictionary<int, Dealership> dealerships = new Dictionary<int, Dealership>();

        /// <summary>
        /// Counter for generating unique IDs for new dealerships.
        /// </summary>
        private static int nextId = 1;

        /// <summary>
        /// Creates a new dealership and adds it to the dictionary.
        /// </summary>
        /// <param name="dealershipCreate">Information for creating the new dealership.</param>
        /// <returns>The newly created dealership.</returns>
        public Dealership CreateDealership(DealershipCreate dealershipCreate)
        {
            var newDealership = new Dealership(nextId, dealershipCreate.Name);

            dealerships.Add(newDealership.Id, newDealership);

            nextId++;

            return newDealership;
        }

        /// <summary>
        /// Searches for vehicles in a dealership based on make, model, and dealership ID.
        /// </summary>
        /// <param name="dealershipSearch">Search parameters including make, model, and dealership ID.</param>
        /// <returns>List of vehicle descriptions matching the search criteria.</returns>
        public List<string> SearchDealership(DealershipSearch dealershipSearch)
        {
            var Make = dealershipSearch.Make;
            var Model = dealershipSearch.Model;
            var DealershipId = dealershipSearch.DealershipId;

            List<string> searchedVehicles = new List<string>();

            if (dealerships.TryGetValue(DealershipId, out var dealership))
            {
                if (dealership.Vehicles.TryGetValue(Make, out var vehicleMakeList))
                {
                    searchedVehicles.AddRange(
                        vehicleMakeList
                            .Where(vehicle => vehicle.Model == Model)
                            .Select(vehicle => vehicle.VehicleDescription)
                    );
                }
            }

            return searchedVehicles;
        }

        /// <summary>
        /// Lists vehicles in a dealership based on the dealership ID.
        /// </summary>
        /// <param name="dealershipList">Information including the dealership ID.</param>
        /// <returns>List of vehicle descriptions in the specified dealership.</returns>
        public List<string> ListVehicles(DealershipList dealershipList)
        {
            var dealerships = DealershipServices.dealerships;

            if (dealerships.TryGetValue(dealershipList.DealershipId, out var dealership))
            {
                var resultList = dealership.Vehicles.SelectMany(make => make.Value.Select(vehicle => vehicle.VehicleDescription)).ToList();
                return resultList.Any() ? resultList : new List<string>(); // Return vehicles if exist, else empty list
            }
            else
            {
                return new List<string>(); // Dealership does not exist
            }
        }
    }
}
