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

        public List<Vehicle> SearchDealership(DealershipSearch dealershipSearch)
        {
            var Make = dealershipSearch.Make;
            var Model = dealershipSearch.Model;
            var DealershipId = dealershipSearch.DealershipId;

            List<Vehicle> searchedVehicle = new List<Vehicle>();

            if (dealerships.TryGetValue(DealershipId, out var dealership))
            {
                if (dealership.Vehicles.TryGetValue(Make, out var vehicleMakeList))
                {
                    searchedVehicle.AddRange(vehicleMakeList.Where(vehicle => vehicle.Model == Model));
                }
            }

            return searchedVehicle;


        }
    }
}

