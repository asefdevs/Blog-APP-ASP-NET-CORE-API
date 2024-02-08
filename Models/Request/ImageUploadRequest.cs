namespace newProject.Models
{
    public class ImageUploadRequest
    {
        public IFormFile File { get; set; }
        public int BlogId { get; set; }
    }
}
