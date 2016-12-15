using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ddac.Models
{
    public class country
    {
        public country()
        {
            this.locations = new HashSet<location>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<location> locations { get; set; }
    }
}