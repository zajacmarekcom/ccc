using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Business
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
    }
}