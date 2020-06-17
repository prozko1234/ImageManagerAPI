using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImageManager.EntityFramework.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ParentId { get; set; }
        public string FilePath { get; set; }
        public string FileLink { get; set; }

        [ForeignKey("ParentId")]
        public Folder ParentFolder { get; set; }
    }
}
