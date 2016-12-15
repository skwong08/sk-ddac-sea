using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ddac.Models.ViewModels
{
    public class TicketIndexVM
    {
        public TicketIndexVM()
        {
            this.DepartureIds = new List<string>();
            this.ArrivalIds = new List<string>();
            this.ShipIds = new List<string>();
        }
        public List<string> DepartureIds { get; set; }
        public List<string> ArrivalIds { get; set; }
        public List<string> ShipIds { get; set; }
        public SelectList DepartureLocation { get; set; }       
        public SelectList ArrivalLocation { get; set; }
        public SelectList Ship { get; set; }
    }
}