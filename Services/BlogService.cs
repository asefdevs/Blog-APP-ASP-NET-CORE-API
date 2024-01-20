using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Http.HttpResults;


namespace newProject.Services
{
    public interface IBlogService
    {
        Task<List<BlogResponse>> GetAllBlogs();
        Task<BlogResponse> GetBlogById(int id);

        Task<BlogResponse> CreateBlog(BlogCreateRequest model); 
        Task<BlogResponse> UpdateBlog(int id,BlogUpdateRequest model);

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
                UserID = model.UserID
            };

            _context.Blogs.Add(blogEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BlogResponse>(blogEntity);

        }

        public async Task<BlogResponse> UpdateBlog(int id,BlogUpdateRequest model)
        {
            var blogEntity = await _context.Blogs.FindAsync(id);

            if (blogEntity == null)
            {
                return null;
            }

            var user_id = blogEntity.UserID;

            if (user_id == null)
            {
                return null;
            }

            blogEntity.Title = model.Title;
            blogEntity.Content = model.Content;
            blogEntity.UpdatedAt = DateTime.Now;


            _context.Blogs.Update(blogEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<BlogResponse>(blogEntity);
        }

        public async Task<BlogResponse> GetBlogById(int id)
        {
            var blog = await _context.Blogs.Include(blog => blog.User).FirstOrDefaultAsync(blog => blog.Id == id);
            return _mapper.Map<BlogResponse>(blog);
        }
    }
}
