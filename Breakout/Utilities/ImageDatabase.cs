using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Utilities;

namespace Breakout.Utilities
{
    public class ImageDatabase
    {
        private static ImageDatabase instance;

        private readonly Dictionary<string, IBaseImage> runtimeImages;

        public static ImageDatabase GetInstance() {
            return ImageDatabase.instance ?? (ImageDatabase.instance = new ImageDatabase());
        }

        private ImageDatabase() {
            runtimeImages = new Dictionary<string, IBaseImage>();
            initialize();
        }

        public static string GetImageFilePath(string filename) {
            string base_path = FileIO.GetProjectPath();
            string path = Path.Combine(base_path, "Assets", "Images", filename);
            return path;
        }

        public void initialize() {

        }

        public IBaseImage GetImage(string imageFile) {
            if (runtimeImages.TryGetValue(imageFile, out IBaseImage img)){
                return img;
            }
            IBaseImage image = loadimage(imageFile);
            return image;
        }
        private IBaseImage loadimage(string imageFile) {
            string imagePath = ImageDatabase.GetImageFilePath(imageFile);
            try {
                if (!File.Exists(imagePath)) {
                    throw new FileNotFoundException($"Error: the image \"{imagePath}\" does not exist.");
                }  
            }
            catch (FileNotFoundException e) {
                System.Console.WriteLine(e);
            }
            IBaseImage image = new Image(imagePath);
            runtimeImages.Add(imageFile, image);
            return image;
        }
    }
}