namespace newProject.Models;
using newProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


public class LoginRequest
{
    [Required(ErrorMessage = "Username is required")]

    public string UserName { get; set; } = null!;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
    

}
