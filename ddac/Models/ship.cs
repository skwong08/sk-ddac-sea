using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ddac.Models
{
    public class ship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Total_Classic_Cabin { get; set; }
        public int Total_Luxury_Cabin { get; set; }
        public int Classic_Cabin_Price { get; set; }
        public int Luxury_Cabin_Price { get; set; }
    }
}