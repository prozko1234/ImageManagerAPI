using System;
using System.Collections.Generic;
using System.Text;
using ImageManager.EntityFramework;
using ImageManager.EntityFramework.Models;
using System.Linq;
using ImageManager.Services.DTOModels;

namespace ImageManager.Services.Repositories.ImageRepository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ImageRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public void AddImage(Image image)
        {
            _applicationContext.Add(image);
            _applicationContext.SaveChanges();
        }

        public List<ImageDTO> GetAllImages()
        {
            var images = _applicationContext.Images.ToList();
            List<ImageDTO> response = new List<ImageDTO>();
            foreach(var item in images)
            {
                response.Add(new ImageDTO
                {
                    Id = item.Id,
                    FilePath = item.FilePath,
                    FileLink = item.FileLink,
                    Name = item.Name,
                    CreatedDate = item.CreatedDate,
                    ParentFolder = item.ParentFolder,
                    Tags = GetPhotosAllTags(item.Id)
                });
            }
            return response;
        }

        public List<string> GetPhotosAllTags(int photoId)
        {
            List<string> tagsList = new List<string>();
            var imageTags = _applicationContext.ImageTags.Where(x => x.Image.Id == photoId).ToList();

            foreach(var item in imageTags)
            {
                tagsList.Add(GetTag(item.Id));
            }
            return tagsList;
        }

        public void GetImage(int id)
        {
            throw new NotImplementedException();
        }

        public void GetImage(string tag)
        {
            throw new NotImplementedException();
        }

        public string GetTag(int id)
        {
            var tag = _applicationContext.HashTags.FirstOrDefault(x => x.Id == id);
            return tag.Tag;
        }

        public void RemoveImage(Image image)
        {
            throw new NotImplementedException();
        }
    }
}
