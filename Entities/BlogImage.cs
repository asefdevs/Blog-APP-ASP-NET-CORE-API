namespace newProject.Entities
{
    public partial class BlogImage
    {
        public int Id { get; set; }

        public string? ImagePath { get; set; }

        public string? ImageUrl { get; set; }

        public int BlogID { get; set; }

        public virtual Blog Blog { get; set; } = null!;
    }
}