using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using ddac.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ddac.Models.ViewModels
{
    public class CreateTripVM
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required]
        public int Days { get; set; }
        public List<string> DepartureIds { get; set; }
        public List<string> ArrivalIds { get; set; }
        public List<string> ShipIds { get; set; }
        [Required]
        public SelectList DepartureLocation { get; set; }
        [Required]
        public SelectList ArrivalLocation { get; set; }
        [Required]
        public SelectList Ship { get; set; }
    }
}