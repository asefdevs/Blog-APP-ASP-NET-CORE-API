using Microsoft.EntityFrameworkCore;
using newProject.Entities;
using newProject.Models;
using AutoMapper;
using System.Threading.Tasks;
using System.Security.Claims;
using newProject.Exceptions;


namespace newProject.Services
{
    public interface IBlogService
    {
        Task<List<BlogResponse>> GetAllBlogs(string searchTerm = null, int? categoryId = null);
        Task<BlogResponse> GetBlogById(int id);

        Task<BlogResponse> CreateBlog(BlogCreateRequest model, int userId); 
        Task<BlogResponse> UpdateBlog(int id, ClaimsPrincipal user, BlogUpdateRequest model);

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

        public async Task<List<BlogResponse>> GetAllBlogs(string searchTerm = null, int? categoryId = null)
        {       
            IQueryable<Blog> query = _context.Blogs
                .Include(blog => blog.User)
                .Include(blog => blog.Category);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(blog => blog.Title.Contains(searchTerm));
            }
            if (categoryId != null)
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    throw new NotFoundException("Category not found");
                }
                query = query.Where(blog => blog.CategoryId == categoryId);
            }
            if (categoryId != null && !string.IsNullOrEmpty(searchTerm))
            {
                var category = await _context.Categories.FindAsync(categoryId);
                if (category == null)
                {
                    throw new NotFoundException("Category not found");
                }
                query = query.Where(blog => blog.CategoryId == categoryId && blog.Title.Contains(searchTerm));
            }
            var blogs = await query.ToListAsync();
            return _mapper.Map<List<BlogResponse>>(blogs);
        }

        public async Task<BlogResponse> CreateBlog(BlogCreateRequest model, int userId)
        {
            var category = await _context.Categories.FindAsync(model.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("Category not found");
            }
          
            var blogEntity = new Blog
            {
                Title = model.Title,
                Content = model.Content,
                UserId = userId,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Blogs.Add(blogEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<BlogResponse>(blogEntity);

        }
       

        public async Task<BlogResponse> UpdateBlog(int id, ClaimsPrincipal user, BlogUpdateRequest model)
        {   var userId = ClaimsHelper.RequestedUser(user);
            var blogEntity = await _context.Blogs.FindAsync(id);
            if (userId != blogEntity.UserId)
            {
                throw new ForbbidenAccessException("You are not author of this blog");
            }

            if (blogEntity == null)
            {
                throw new NotFoundException("Blog not found");
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                blogEntity.Title = model.Title;
            }
            if (!string.IsNullOrEmpty(model.Content))
            {
                blogEntity.Content = model.Content;
            }
            blogEntity.UpdatedAt = DateTime.Now;

            _context.Blogs.Update(blogEntity);
            await _context.SaveChangesAsync();


            return _mapper.Map<BlogResponse>(blogEntity);
        }

        public async Task<BlogResponse> GetBlogById(int id)
        {
            var blog = await _context.Blogs
            .Include(blog => blog.User)
            .Include(blog => blog.Category)
            .FirstOrDefaultAsync(blog => blog.Id == id);
            if (blog == null)
            {
                throw new NotFoundException("Blog not found");
            }
            return _mapper.Map<BlogResponse>(blog);
        }
    }
}
