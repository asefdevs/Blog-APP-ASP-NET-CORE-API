using AutoMapper;
using newProject.Entities;
using newProject.Helpers.Dto;
using newProject.Models;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Blog, BlogResponse>().ReverseMap();

        CreateMap<User, UserResponse>().ReverseMap();

        CreateMap<Comment, CommentResponse>().ReverseMap();

        CreateMap<Category, CategoryDto>().ReverseMap();

    }
}
