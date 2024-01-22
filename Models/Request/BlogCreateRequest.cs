namespace newProject.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class BlogCreateRequest
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50,ErrorMessage = "Title can't be longer than 50 characters")]
    public string Title { get; set; }

    public string Content { get; set; } 

    public int UserID { get; set; }
}