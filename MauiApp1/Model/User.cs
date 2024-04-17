using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;


namespace MauiApp1.Model
{
    public class User
    {

        public int userId { get; }
        public string name { get; }
        public string profilePhotoPath { get; }


        public User(int userId, string name, string profilePhotoPath)
        {
            this.userId = userId;
            this.name = name;
            this.profilePhotoPath = profilePhotoPath;
        }
    }
}


