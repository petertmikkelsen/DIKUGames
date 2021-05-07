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

namespace Breakout {
    public class LevelLoader {
        public string[] file { private set; get; }
        public string[] map { private set; get; }
        public string[] meta { private set; get; }
        public string[] legend { private set; get; }
        Dictionary<string, string> blocksDictionary =
            new Dictionary<string, string>();
        public EntityContainer<Block> blocks = new EntityContainer<Block>();
        
        // Breaks file into three subfiles
        public string[] CreateSubFile(string subFileName) {
            var firstWord = subFileName + ":";
            var lastWord = subFileName + "/";
            if (Array.Exists(file, element => element == firstWord) && Array.Exists(file, element => element == lastWord)) {
                var subFileStarts = Array.IndexOf(file, firstWord) + 1;
                var subFileEnds = Array.IndexOf(file, lastWord);
                string[] arr = new string[subFileEnds - subFileStarts]; 
                for (int i = subFileStarts; i < subFileEnds; i++)
                    arr[i - subFileStarts] = file[i];
                return arr;
            } else return new string[0]; 
        }

        // Calls CreateSubFile
        public void CreateAllSubFiles() {
            map = CreateSubFile("Map");
            meta = CreateSubFile("Meta");
            legend = CreateSubFile("Legend");
        }

        // Creates dictionary based on legend file
        public void legendToDictionary() {
            foreach (string i in legend) {
                blocksDictionary.Add(i.Substring(0, 1), i.Substring(3));
            }

        }

        // Creates a block and add it to the block list
        public void CreateBlock (Vec2F position, string image) {
            blocks.AddEntity(new Block(
                new StationaryShape(position, new Vec2F(0.08f, 0.03f)),
                new Image(Path.Combine("Assets", "Images",  image))));
        }
 

        // Calls CreateBlock for each block in map, with correct(?) position
        public void LevelBuilder () {
            for (int i = 0; i < map.Length; i ++) {
                for (int j = 0; j < map[i].Length; j++) {
                    if (map[i][j] != '-') {
                        // Console.Write(".");
                        string c = Char.ToString(map[i][j]);
                        CreateBlock(new Vec2F(0.02f + j * 0.08f, 0.95f - 
                            i * 0.03f), blocksDictionary[c]);
                    }                        
                }
            }
        }

        // Calls the above functions in correct order
        public void LoadNewlevel(string filename) {
            file = File.ReadAllLines(filename);
            CreateAllSubFiles();
            legendToDictionary();
            Console.WriteLine(blocksDictionary["#"]);
            LevelBuilder();

        }
                
        
        // Starts with, ends with, index of - Google
        // Dictionary - Google
        // public LevelLoader(string[] LevelName) {
        //     foreach (string lines in level1) {
        //         for (int i = 1; i < 26; i++) {
        //             map.Add(i);
        //         }
        //     }
        // }
    }
}