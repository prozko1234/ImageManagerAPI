using ImageManager.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Services.DTOModels
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public UserRoles Role { get; set; }
    }
}
