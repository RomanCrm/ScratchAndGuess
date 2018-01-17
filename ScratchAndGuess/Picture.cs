using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScratchAndGuess
{
    class Picture
    {
        Random rnd = new Random();
        int last = 0;
        int idxPict = 0;
        public int IdxPict
        {
            get => idxPict;
            private set => idxPict = value;
        }

        private string extension = ".png";
        public string Extension
        {
            get => extension;
        }

        private string pathFolder = "img";
        public string PathFolder
        {
            get => pathFolder;
        }

        List<Image> images = new List<Image>();
        public List<Image> Images
        {
            get => images;
        }

        public Picture()
        {
            InitializeImages();
        }

        private void InitializeImages()
        {
            string pathImg = null;

            string currentDir = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(currentDir);

            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo directory in dirs)
            {
                if (directory.Name == PathFolder)
                {
                    pathImg = directory.FullName;
                }
            }

            DirectoryInfo img = new DirectoryInfo(pathImg);
            FileInfo[] files = img.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == Extension)
                {
                    images.Add(Image.FromFile(files[i].FullName));
                    images[i].Tag = files[i].Name.Replace(Extension, "");
                }
            }
        }

        public void RandNewPict(ref TextureBrush textureBrush, ref Region region, ref Picture picture, Size clientSize)
        {
            idxPict = rnd.Next(0, images.Count);
            while (last == idxPict)
            {
                idxPict = rnd.Next(0, images.Count);
            }
            last = idxPict;

            textureBrush = new TextureBrush(Image.FromFile(PathFolder + "/" + images[idxPict].Tag.ToString() + Extension));
            textureBrush.WrapMode = System.Drawing.Drawing2D.WrapMode.Clamp;
            textureBrush.TranslateTransform(clientSize.Width / (int)Locations.Quart, 40f);
            Rectangle rectanglePicture = new Rectangle(clientSize.Width / (int)Locations.Quart, 40, picture.Images[0].Width,
                                                       picture.Images[0].Height);
            region = new Region(rectanglePicture);
        }

    }
}
