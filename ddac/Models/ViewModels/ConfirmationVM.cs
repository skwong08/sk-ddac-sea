using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ddac.Models.ViewModels
{
    public class ConfirmationVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Contact { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime StartDate { get; set; }
        public int DayDuration { get; set; }
        public string shipName { get; set; }
    }
}