using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}