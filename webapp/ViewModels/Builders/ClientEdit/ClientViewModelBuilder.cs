using AutoMapper;
using webapp.Models;
using webapp.ViewModels.Client;

namespace webapp.ViewModels.Builders.ClientEdit
{
    public class ClientViewModelBuilder
    {
        public static StatusNotesViewModel CreateStatusNotesViewModel(ClientStep8 data, int? visitId, bool canBeGreen)
        {
            var model = data != null ? Mapper.Map<StatusNotesViewModel>(data) : new StatusNotesViewModel();

            model.CanBeGreen = canBeGreen;
            model.Edit = data != null;
            model.VisitId = visitId.Value;

            return model;
        }
    }
}
