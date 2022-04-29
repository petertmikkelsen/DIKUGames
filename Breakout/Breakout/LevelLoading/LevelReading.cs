using System;
using System.IO;
using Breakout.BreakoutStates;
using Breakout.LevelHandling;
using DIKUArcade.Events;

namespace Breakout.LevelLoading {
    class LevelReading {
        private string[] file;
        public string[] map;
        public string[] meta;
        public string[] legend;
        private Dictionary<string, string> metaDictionary =
            new Dictionary<string, string>();
        private Dictionary<string, string> legendDictionary =
            new Dictionary<string, string>();

        public void CreateSubFiles() {
            map = CreateSubFiles("Map");
            meta = CreateSubFiles("Meta");
            legend = CreateSubFiles("Legend");
        }

        public string[] CreateSubFiles(string subFile) { //Loads the data from the txt files (subFile could be "Map")
            string firstChar = subFile + ":"; //Every chunk of data starts with the subFile name and : (e.g. Map: in the txt file)
            string finalChar = subFile + "/"; //The chunk ends with the subFile name and / (e.g. Map/)

            if (Array.Exists(file, element => element == firstChar) && Array.Exists(file, element => element == finalChar)) { //If a correct firstChar and finalChar (with same name and : and /) exists, then execute the following
                var subFileFirst = Array.IndexOf(file, firstChar) + 1; //+1 because 0-indexed
                var subFileFinal = Array.IndexOf(file, finalChar);
                string[] array = new string[subFileFinal - subFileFirst]; //Correct array length
                for (int i = subFileFirst; i < subFileFinal; i++) { //Commit every element in the subFile to an array
                    array[i - subFileFirst] = file[i]; //-subFileFirst because 0-indexed
                }
                return array;
            } else return new string[0]; 
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
        public LevelDefinition LoadDefinition(LevelEnum level) {
            var filename = LevelTransformer.LevelToStr(level);
            file = File.ReadAllLines(filename);
            CreateSubFiles();
            metaToDictionary();
            legendToDictionary();
            return new LevelDefinition(map, metaDictionary, legendDictionary);
        }
    }
}