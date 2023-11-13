using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace api.Data
{
        public class Vehicle
        {
        public string vehicleDescription;

                [Required]
                [MaxLength(50)]
                public string Make { get; set; }
                [Required]
                [MaxLength(50)]
                public string Model { get; set; }
                [Required]
                [Range(1908, 2050)]
                public int ModelYear { get; set; }


                [Required]
                [ForeignKey(nameof(Dealership))]
                public int DealershipId { get; set; }
                public Dealership Dealership { get; set; }

                public int Stock { get; set; }

                public Vehicle(string _Make, string _Model, int _ModelYear, int _DealershipId, Dealership _Dealership, int _Stock)
                {
                        Make = _Make;
                        Model = _Model;
                        ModelYear = _ModelYear;
                        DealershipId = _DealershipId;
                        Dealership = _Dealership; 
                        Stock = _Stock;
                        

                }


                public string VehicleDescription
                {
                        get
                        {
                                vehicleDescription = $"{ModelYear} {Make} {Model} : Stock Level {Stock}";
                                return vehicleDescription;
                        }
                        set { vehicleDescription = value; }
                }


        }
        }