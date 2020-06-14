using ImageManager.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Services.Repositories.ImageRepository
{
    public interface IImageRepository
    {
        void AddImage(Image image);
        void RemoveImage(Image image);
        void GetImage(int id);
        void GetImage(string tag);
        void GetAllImages();
    }
}
