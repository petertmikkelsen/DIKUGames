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
        // private static ImageDatabase instance;

        // private readonly Dictionary<string, IBaseImage> runtimeImages;
// 
        // public static ImageDatabase GetInstance() {
        //     return ImageDatabase.instance ?? (ImageDatabase.instance = new ImageDatabase());
        // }

        // private ImageDatebase() {
        //     runtimeImages = new Dictionary<string, IBaseImage>();
        //     
        // }

        public static string GetImageFilePath(string filename) {
            string base_path = FileIO.GetProjectPath();
            string path = Path.Combine(base_path, "Assets", "Images", filename);
            return path;
        }


    }
}