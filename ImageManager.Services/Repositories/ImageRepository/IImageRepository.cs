using ImageManager.EntityFramework.Models;
using ImageManager.Services.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Services.Repositories.ImageRepository
{
    public interface IImageRepository
    {
        void AddImage(Image image);
        void RemoveImage(Image image);
        ImageDTO GetImage(int id);
        List<ImageDTO> GetAllImages();
        List<string> GetPhotosAllTags(int photoId);
        string GetTag(int id);
    }
}
