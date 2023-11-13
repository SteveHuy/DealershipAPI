using api.Models.Dealership;
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
    public class DealershipServices
    {
        public static Dictionary<int, Dealership> dealerships = new Dictionary<int, Dealership>();

        private static int nextId = 1;

        public Dealership CreateDealership(DealershipCreate dealershipCreate)
        {
            var newDealership = new Dealership(nextId, dealershipCreate.Name);

            dealerships.Add(newDealership.Id, newDealership);

            nextId++;

            return newDealership;
        }

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

