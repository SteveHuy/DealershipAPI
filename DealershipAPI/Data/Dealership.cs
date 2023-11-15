using System.ComponentModel.DataAnnotations;

namespace api.Data

{
    public class Dealership{
        public int Id { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }

        public Dictionary<string, List<Vehicle>> Vehicles { get; set; } = new Dictionary<string, List<Vehicle>>();

        public Dealership(int _id, string _name){
            Name = _name;
            Id = _id;
            Vehicles = new Dictionary<string, List<Vehicle>>();

        }

    }
}