using System;
using System.Collections.Generic;
using System.Text;

namespace ImageManager.Logic.Models
{
    public class HashingObject
    {
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
