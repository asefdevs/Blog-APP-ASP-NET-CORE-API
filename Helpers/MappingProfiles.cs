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

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserProfiles.FirstOrDefault().FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserProfiles.FirstOrDefault().LastName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.UserProfiles.FirstOrDefault().BirthDate))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.UserProfiles.FirstOrDefault().Country))
            .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.UserProfiles.FirstOrDefault().Bio));

        CreateMap<Comment, CommentResponse>().ReverseMap();

        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<Image, ImageUploadResponse>()
            .ForMember(dest => dest.ImageName, opt => opt.MapFrom(src => src.ImageName))
            .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
            .ReverseMap();

    }
}
