using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImageManager.EntityFramework.Models
{
    public class Folder
    {   [Key]
        public int Id { get; set; }
        [Required]
        public int Name { get; set; }
    }
}
