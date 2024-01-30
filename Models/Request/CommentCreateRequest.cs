namespace newProject.Models
{
    public class CommentCreateRequest
    {
        public int BlogId { get; set; }
        public string Content { get; set; } = null!;
    }
}