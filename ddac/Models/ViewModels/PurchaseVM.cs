using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ddac.Models.ViewModels
{
    public class PurchaseVM
    {
        public int tripId { get; set; }
        public string shipName { get; set; }
        public int Total_Classic_Cabin { get; set; }
        public int Total_Luxury_Cabin { get; set; }
        public int Classic_Cabin_Price { get; set; }
        public int Luxury_Cabin_Price { get; set; }
    }
}