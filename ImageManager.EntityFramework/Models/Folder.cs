using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImageManager.EntityFramework.Models
{
    public class Folder
    {   [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User User { get; set; }
    }
}
