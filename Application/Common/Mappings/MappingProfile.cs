using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(d => d.TotalTasks, opt => opt.MapFrom(s => s.Tasks.Count))
                .ForMember(d => d.CompletedTasks, opt => opt.MapFrom(s => s.Tasks.Count(t => t.Status == ProjectTaskStatus.Done)));

            CreateMap<ProjectTask, ProjectTaskDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => s.Priority.Label));

            CreateMap<User, UserSummaryDto>();

            CreateMap<Comment, CommentDto>();
        }
    }
}
