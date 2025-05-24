using AutoMapper;
using TaskFlow.Application.Common.Models;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Common.Mapping;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<TaskItem, TaskDto>()
            .ForMember(e => e.ProjectName, opt => opt.MapFrom(s => s.Project.Name))
            .ForMember(e => e.AssigneeName, opt => opt.MapFrom(s => s.Assignee!.FullName));
        
        CreateMap<Project, ProjectDto>()
            .ForMember(e => e.OwnerName, opt => opt.MapFrom(s => s.Owner.FullName))
            .ForMember(e => e.TeamName, opt => opt.MapFrom(s => s.Team!.Name))
            .ForMember(e => e.TaskCount, opt => opt.MapFrom(s => s.Tasks.Count));

        CreateMap<User, UserDto>()
            .ForMember(e => e.FullName, opt => opt.MapFrom(s => s.FullName));
    }
}