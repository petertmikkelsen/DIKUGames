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
        private string[] file;
        public string[] map { private set; get; }
        public string[] meta { private set; get; }
        public string[] legend { private set; get; }
        private Dictionary<string, string> metaDictionary =
            new Dictionary<string, string>();
        private Dictionary<string, string> legendDictionary =
            new Dictionary<string, string>();
        private EntityContainer<Block> blocks = new EntityContainer<Block>();
        
        // Breaks file into three subfiles
        private string[] CreateSubFile(string subFileName) {
            var firstWord = subFileName + ":";
            var lastWord = subFileName + "/";
            if (Array.Exists(file, element => element == firstWord) 
                && Array.Exists(file, element => element == lastWord)) {
                    var subFileStarts = Array.IndexOf(file, firstWord) + 1;
                    var subFileEnds = Array.IndexOf(file, lastWord);
                    string[] arr = new string[subFileEnds - subFileStarts]; 
                    for (int i = subFileStarts; i < subFileEnds; i++)
                        arr[i - subFileStarts] = file[i];
                    return arr;
            } else return new string[0]; 
        }

        // Calls CreateSubFile
        private void CreateAllSubFiles() {
            map = CreateSubFile("Map");
            meta = CreateSubFile("Meta");
            legend = CreateSubFile("Legend");
        }

        // Creates dictionary based on map file
        private void metaToDictionary() {
            foreach (string i in meta) {
                if (i.Contains(":")) {
                    int middle;
                    middle = i.IndexOf(":");
                    metaDictionary.Add(i.Substring(0, middle), (i.Substring(middle+2)));
                }
            }
        }

        // Creates dictionary based on legend file
        private void legendToDictionary() {
            foreach (string i in legend) {
                legendDictionary.Add(i.Substring(0, 1), i.Substring(3));
            }
        }

        public (string[], Dictionary<string, string>, Dictionary<string, string>) GetSubfiles(string levelAsASCII) {
                file = File.ReadAllLines(levelAsASCII);
                CreateAllSubFiles();
                metaToDictionary();
                legendToDictionary();
                return (map, metaDictionary, legendDictionary);
        }
    }
}