using AutoMapper;
using newProject.Entities;
using newProject.Helpers.Dto;
using newProject.Models;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Blog, BlogResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ReverseMap();

        CreateMap<User, UserResponse>().ReverseMap();

        CreateMap<Comment, CommentResponse>().ReverseMap();

        CreateMap<Category, CategoryDto>().ReverseMap();

    }
}
