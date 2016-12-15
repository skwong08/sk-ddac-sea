using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ddac.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime StartDate { get; set; }
        public int DayDuration { get; set; }
    }
}