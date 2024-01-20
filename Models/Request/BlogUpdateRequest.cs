namespace newProject.Models;
using newProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class BlogUpdateRequest
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50,ErrorMessage = "Title can't be longer than 50 characters")]
    public string Title { get; set; }
    public string Content { get; set; } 

}