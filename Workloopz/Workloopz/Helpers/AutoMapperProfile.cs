using AutoMapper;
using Workloopz.Data;
using Workloopz.ViewModels;

namespace Workloopz.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapping giữa ViewModel và Entity
            CreateMap<RegisterVM, User>();
            CreateMap<ProjectVM, Project>();
            CreateMap<TaskVM, Data.Task>();

            CreateMap<CalendarVM, Data.Task>()
            .ForMember(task => task.Id, option => option.MapFrom(calendar => calendar.id))
            .ForMember(task => task.Tittle, option => option.MapFrom(calendar => calendar.title))
            .ForMember(task => task.ScheduledTime, option => option.MapFrom(calendar => calendar.start))
            .ForMember(task => task.ScheduledEnd, option => option.MapFrom(calendar => calendar.end ))
            .ForMember(task => task.Description, option => option.MapFrom(calendar => calendar.description))
            .ForMember(task => task.Color, option => option.MapFrom(calendar => calendar.color))
            .ReverseMap();

            CreateMap<KanbanVM, Data.Task>()
                .ForMember(task => task.Id, option => option.MapFrom(kanban => kanban.id))
                .ForMember(task => task.Tittle, option => option.MapFrom(kanban => kanban.title))
                .ForMember(task => task.Description, option => option.MapFrom(kanban => kanban.description))
                .ForMember(task => task.ScheduledEnd, option => option.MapFrom(kanban => kanban.end))
                .ForMember(task => task.StatusId, option => option.MapFrom(kanban => kanban.status))
                .ReverseMap();

        }
    }
}
