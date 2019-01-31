using System;
using System.Collections.Generic;

namespace VolvoTrucks.Models
{
    public partial class TruckModels
    {
        public TruckModels()
        {
            Trucks = new HashSet<Trucks>();
        }

        public int ModelId { get; set; }
        public int ModelYear { get; set; }
        public string ModelName { get; set; }

        public virtual ICollection<Trucks> Trucks { get; set; }
    }
}
