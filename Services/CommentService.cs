using newProject.Models;
using System.Collections.Generic;
using newProject.Entities;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Security.Claims;
using newProject.Exceptions;


namespace newProject.Services
{
    public interface ICommentService
    {
        Task<List<CommentResponse>> AllComments(int blogId);
        Task<CommentResponse> CreateComment(CommentCreateRequest model, int userId);
        // Task<CommentResponse> UpdateComment(int id, CommentUpdateRequest model);
    }

    public class CommentService : ICommentService
    {
        private readonly MyprojectdbContext _context;
        private readonly IMapper _mapper;

        public CommentService(MyprojectdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CommentResponse>> AllComments(int blogId)
        {
            var comments = await _context.Comments.Where(x => x.BlogId == blogId).ToListAsync();
            return _mapper.Map<List<CommentResponse>>(comments);
        }

        public async Task<CommentResponse> CreateComment(CommentCreateRequest model, int userId)
        {
            var commentEntity = new Comment
            {
                Context = model.Content,
                UserId = userId,
                BlogId = model.BlogId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CommentResponse>(commentEntity);
        }

    }
}