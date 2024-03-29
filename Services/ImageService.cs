using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;

namespace newProject.Services
{
    public interface IImageService
    {
        Task<ImageUploadResponse> UploadImage(ImageUploadRequest model, int userId);
    }

    public class ImageService:IImageService
    {
        private readonly MyprojectdbContext _context;
        private readonly IMapper _mapper;

    public ImageService(MyprojectdbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }


    public async Task<ImageUploadResponse> UploadImage(ImageUploadRequest model, int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        var blog = await _context.Blogs.FindAsync(model.BlogId);
        if (blog == null)
        {
            throw new Exception("Blog not found");
        }
        if (blog.User != user)
        {
            throw new Exception("You are not allowed to upload image for this blog");
        }
        if (model.File == null)
        {
            throw new Exception("Image is required");
        }


        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
        var filePath = Path.Combine("wwwroot", "images", fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);
        }

        var image = new Image
        {
            ImageName = fileName,
            ImagePath = filePath,
            BlogId = model.BlogId
        };

        _context.Images.Add(image);
        await _context.SaveChangesAsync();
        return _mapper.Map<ImageUploadResponse>(image);

    }
}
}