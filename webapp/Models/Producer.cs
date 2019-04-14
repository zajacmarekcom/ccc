using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Country { get; set; }
        public int Group { get; set; }
    }
}