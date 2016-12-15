using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using ddac.Models;
using System.ComponentModel.DataAnnotations;

namespace ddac.Models.ViewModels
{
    public class CreateCountryVM
    {
        [Required]
        public string Name { get; set; }
        public List<CheckBoxListItem> Locations { get; set; }
        public CreateCountryVM()
        {
            Locations = new List<CheckBoxListItem>();
        }
    }
}