using newProject.Entities;

namespace newProject.Helpers.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }
}