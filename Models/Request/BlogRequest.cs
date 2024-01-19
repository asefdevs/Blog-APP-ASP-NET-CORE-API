namespace newProject.Models;
using newProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


public class BlogCreateRequest
{
    public string Title { get; set; }

    public string Content { get; set; } 

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserId { get; set; }
}