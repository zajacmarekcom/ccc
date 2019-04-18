using AutoMapper;
using webapp.Enums;
using webapp.Models;
using webapp.ViewModels.Client;

namespace webapp.App_Start
{
    public class AutomapperConfig
    {
        public static void Init()
        {
            CreateClientMapping();
        }

        private static void CreateClientMapping()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<Note, NoteViewModel>();
                    cfg.CreateMap<ClientStep8, StatusNotesViewModel>()
                        .ForMember(vm => vm.Status, conf => conf.MapFrom(e => (BusinessStatus)e.Status))
                        .ForMember(vm => vm.Edit, conf => conf.Ignore());
                    cfg.CreateMap<NoteViewModel, Note>();
                    cfg.CreateMap<StatusNotesViewModel, ClientStep8>()
                        .ForMember(e => e.Status, conf => conf.MapFrom(vm => (int)vm.Status));
                }
            );
        }
    }
}
