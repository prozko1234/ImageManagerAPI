using ImageManager.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Services.DTOModels
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Folder ParentFolder { get; set; }
        public string FilePath { get; set; }
        public string FileLink { get; set; }
        public List<string> Tags { get; set; }
    }
}
