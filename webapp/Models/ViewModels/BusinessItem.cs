using System;
using webapp.Enums;

namespace webapp.Models.ViewModels
{
    public class BusinessItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BusinessStatus Status { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime VisitDate { get; set; }
    }
}