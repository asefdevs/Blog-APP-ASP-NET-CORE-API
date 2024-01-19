using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;
using System;


namespace newProject.Services
{
    public interface IBlogService
    {
        Task<List<BlogResponse>> GetAllBlogs();
        Task<BlogResponse> CreateBlog(BlogCreateRequest model); 
    }

    public class BlogService : IBlogService
    {
        private readonly MyprojectdbContext _context;
        private readonly IMapper _mapper;

        public BlogService(MyprojectdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BlogResponse>> GetAllBlogs()
        {
            var blogs = await _context.Blogs.Include(blog => blog.User).ToListAsync();
            return _mapper.Map<List<BlogResponse>>(blogs);
        }

        public async Task<BlogResponse> CreateBlog(BlogCreateRequest model)
        {
            var blogEntity = new Blog
            {
                Title = model.Title,
                Content = model.Content,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                UserId = model.UserId
            };

            _context.Blogs.Add(blogEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BlogResponse>(blogEntity);
            
        }
    }
}
