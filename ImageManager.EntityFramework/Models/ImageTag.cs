using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImageManager.EntityFramework.Models
{
    public class ImageTag
    {
        [Key]
        public int Id { get; set; }
        public int TagId { get; set; }
        public int ImageId { get; set; }
        [ForeignKey("TagId ")]
        public HashTag Tag { get; set; }
        [ForeignKey("ImageId ")]
        public Image Image { get; set; }
    }
}
