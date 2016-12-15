using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ddac.Models
{
    public class Trip
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        public location DepartureLocation { get; set; }
        public location ArrivalLocation { get; set; }
        public ship Ships { get; set; }
        public string dName { get; set; }
        public string aName { get; set; }
        public string sName { get; set; }
    }
}