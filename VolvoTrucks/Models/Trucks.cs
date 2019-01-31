using System;
using System.Collections.Generic;

namespace VolvoTrucks.Models
{
    public partial class Trucks
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public string Chassis { get; set; }
        public int MaximumLoad { get; set; }

        public virtual TruckModels Model { get; set; }
    }
}
