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
            CreateMap<TaskVM, Workloopz.Data.Task>();

            //// Mapping GanttTaskVM và Task với định dạng tùy chỉnh
            //CreateMap<Workloopz.Data.Task, GanttTaskVM>()
            //    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Tittle))
            //    .ForMember(dest => dest.start_date, opt => opt.MapFrom(src => src.ScheduledTime.HasValue ? src.ScheduledTime.Value.ToString("yyyy-MM-dd") : ""))
            //    .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
            //    .ForMember(dest => dest.Progress, opt => opt.MapFrom(src => src.Progress))
            //    .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.ParentId));

            //// Mapping cho LinkVM và Link với ánh xạ thuộc tính tùy chỉnh
            //CreateMap<Link, LinkVM>()
            //    .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.SourceTaskId))
            //    .ForMember(dest => dest.Target, opt => opt.MapFrom(src => src.TargetTaskId))
            //    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));
        }
    }
}
