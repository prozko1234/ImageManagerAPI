using NUnit.Framework;
using Moq;
using ImageManager.Services.Repositories.ImageRepository;
using ImageManager.Services.DTOModels;
using System;
using ImageManager.EntityFramework.Models;
using ImageManager.EntityFramework;
using System.Collections.Generic;
using ImageManager.Services.Repositories.AccountRepository;

namespace ImageManager.Tests
{
    public class Tests
    {
        private ImageDTO image;
        private UserDTO user;
        private UserDTO wrongUser;
        private List<ImageDTO> allImages = new List<ImageDTO>();
        Mock<IImageRepository> imageRep = new Mock<IImageRepository>();
        Mock<IAccountRepository> accountRep = new Mock<IAccountRepository>();

        [SetUp]
        public void Setup()
        {
            image = new ImageDTO
            {
                Id = 14,
                FilePath = "D:\\PROJECTS\\ImageManager\\ImageManager\\ImageManager\\public/images\ava.png",
                FileLink = "https://localhost:44331/images/ava.png",
                Name = "ava.png",
                CreatedDate = DateTime.Parse("2020-06-17 20:57:13.5770999"),
                ParentFolder = new Folder { Id = 1, Name = "root", OwnerId = 1 },
                Tags = new List<string> { "cat" }
            };

            allImages.Add(new ImageDTO {
                Id = 14,
                FilePath = "D:\\PROJECTS\\ImageManager\\ImageManager\\ImageManager\\public/images\ava.png",
                FileLink = "https://localhost:44331/images/ava.png",
                Name = "ava.png",
                CreatedDate = DateTime.Parse("2020-06-17 20:57:13.5770999"),
                ParentFolder = new Folder { Id = 1, Name = "root", OwnerId = 1 },
                Tags = new List<string> { "cat" }
            });
            allImages.Add(new ImageDTO
            {
                Id = 15,
                FilePath = "D:\\PROJECTS\\ImageManager\\ImageManager\\ImageManager\\public/images\\7c25be23be2e1ce5020291ade72e3e80.jpg",
                FileLink = "https://localhost:44331/images/7c25be23be2e1ce5020291ade72e3e80.jpg",
                Name = "7c25be23be2e1ce5020291ade72e3e80.jpg",
                CreatedDate = DateTime.Parse("2020-06-18 00:30:50.1582531"),
                ParentFolder = new Folder { Id = 1, Name = "root", OwnerId = 1 },
                Tags = new List<string>()
            });
            user = new UserDTO { Email = "admin@mail.com", Login = "admin", Role = UserRoles.Admin };
            wrongUser = new UserDTO { Email = "admin@mail.com", Login = "wrongAdmin", Role = UserRoles.Admin };
        }

        [Test]
        public void ShouldReturnImage()
        {
            imageRep.Setup(p => p.GetImage(14)).Returns(image);
            var result = imageRep.Object.GetImage(14);
            Assert.AreEqual(result, image);       
        }


        [Test]
        public void ShouldReturnImages()
        {
            imageRep.Setup(p => p.GetAllImages()).Returns(allImages);
            var result = imageRep.Object.GetAllImages();
            Assert.AreEqual(result, allImages);
        }

        [Test]
        public void ShouldReturnProfile()
        {
            accountRep.Setup(p => p.GetProfile("admin")).Returns(user);
            var result = accountRep.Object.GetProfile("admin");
            Assert.AreEqual(result, user);
        }

        [Test]
        public void ShouldReturnNullNotProfile()
        {
            accountRep.Setup(p => p.GetProfile("admin")).Returns(wrongUser);
            var result = accountRep.Object.GetProfile("admin");
            Assert.AreNotEqual(result, user);
        }
    }
}