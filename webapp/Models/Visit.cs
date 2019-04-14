using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class Visit
    {
        public int Id { get; set; }
        [Display(Name = "Data wizyty")]
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }
        [Display(Name="Segment rynku wiodący")]
        public int MainMarketSegmentId { get; set; }
        [Display(Name = "Uwagi")]
        public string Comments { get; set; }
        public int BusinessId { get; set; }
    }
}