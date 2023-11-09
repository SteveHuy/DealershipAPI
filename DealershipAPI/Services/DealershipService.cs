using api.Models.Dealership;
using api.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // if there are situations which would result in a early break such as cannot find dealershipid we return empty list


            if (dealerships.ContainsKey(DealershipId))
            {
                var dealership = dealerships[DealershipId]; // picking the right dealership from the list of dealerships



                var vehicleList = dealership.Vehicles;
                if (vehicleList.Count == 0)
                {
                    return searchedVehicle; // there are no vehicles present
            }
                if (vehicleList.ContainsKey(Make))
                {
                    var vehicleMakeList = vehicleList[Make];

                    foreach (Vehicle vehicle in vehicleMakeList)
                    {
                      
                        if (vehicle.Model == Model)
                        {
                            Console.WriteLine("I am the problem");
                            searchedVehicle.Add(vehicle);
                        }
                    }
                    if (searchedVehicle.Count == 0)
                    {
                        return searchedVehicle; // none of the given the given model
                    }
                    else
                    {
                        return searchedVehicle; // there are vehicles with that make and model!
                    }
                }
                else
                {
                    return searchedVehicle; // none for the given make
                }

            }
            else
            {
                return searchedVehicle; // Dealership id doesn't exist
            }
        }
    }


}

