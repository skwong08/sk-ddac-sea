using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ddac.Models
{
    public class cruise
    {
        public cruise()
        {
            this.ships = new HashSet<ship>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ship> ships { get; set; }
    }
}