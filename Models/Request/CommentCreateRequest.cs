namespace newProject.Models
{
    public class CommentCreateRequest
    {
        public int BlogId { get; set; }
        public string Context { get; set; } = null!;
    }
}