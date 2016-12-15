using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using ddac.Models;
using System.ComponentModel.DataAnnotations;

namespace ddac.Models.ViewModels
{
    public class CreateCruiseVM
    {
        [Required]
        public string Name { get; set; }
        public List<CheckBoxListItem> Ships { get; set; }
        public CreateCruiseVM()
        {
            Ships = new List<CheckBoxListItem>();
        }
    }
}