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

        public List<Vehicle> ListVehicles(DealershipList dealershipList)
        {
            var id = dealershipList.DealershipId;
            var dealerships = DealershipServices.dealerships;
            List<Vehicle> resultList = new List<Vehicle>();

            if (dealerships.ContainsKey(id))
            {
                Dictionary<string, List<Vehicle>> vehicleList = dealerships[id].Vehicles;


                if (vehicleList.Count() == 0)
                {
                    return resultList; // No vehicles exist
                }
                else
                {
                    foreach (var make in vehicleList)
                    {
                        foreach (Vehicle vehicle in make.Value)
                        {
                            resultList.Add(vehicle);
                        };
                    }
                    return resultList; // return the list of vehicles
                }
            }
            else
            {
                return resultList; // Dealership does not exist
            }
        }

        public string JsonSerializerList(List<Vehicle> list)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var jsonString = JsonSerializer.Serialize(list, options);

            return jsonString;

        }


    }
}

