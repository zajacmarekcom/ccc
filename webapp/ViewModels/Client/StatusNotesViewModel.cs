using System;
using System.Collections.Generic;
using webapp.Enums;

namespace webapp.ViewModels.Client
{
    public class StatusNotesViewModel
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public bool Edit { get; set; }
        public bool CanBeGreen { get; set; }
        public DateTime? NewVisitDate { get; set; }
        public BusinessStatus Status { get; set; }
        public IEnumerable<NoteViewModel> Notes { get; set; }
    }
}
