using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class SelectedManufacturers
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public bool Checked { get; set; }
        public int Percent { get; set; }
    }
}