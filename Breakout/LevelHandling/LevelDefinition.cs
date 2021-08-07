using System;
using System.Collections.Generic;
using Breakout.Blocks;

namespace Breakout.LevelHandling
{
    public class LevelDefinition {
        public string[] map;
        public Dictionary<string, string> metaDictionary;
        public Dictionary<string, string> legendDictionary;
        public Dictionary<string, BlockEnum> blockTypeDictionary;
        public LevelDefinition(string[] map, Dictionary<string, string> metaDictionary, Dictionary<string, string> legendDictionary) {
            this.map = map;
            this.metaDictionary = metaDictionary;
            this.legendDictionary = legendDictionary;
            blockTypeDictionary = new Dictionary<string, BlockEnum>();
            InitializeBlockDictionary();
        }
        private void InitializeBlockDictionary() {
            foreach (var pair in metaDictionary) {
                if (pair.Key == "PowerUp") {
                    blockTypeDictionary[pair.Value] = BlockEnum.PowerUpBlock;
                }
                if (pair.Key == "Unbreakable") {
                    blockTypeDictionary[pair.Value] = BlockEnum.UnbreakableBlock;
                }
                if (pair.Key == "Hardened") {
                    blockTypeDictionary[pair.Value] = BlockEnum.HardenedBlock;
                }
                if (pair.Key == "Explosive") {
                    blockTypeDictionary[pair.Value] = BlockEnum.ExplosiveBlock;
                }
            }
        }
        public BlockEnum GetBlockType(string c) {
            if (!blockTypeDictionary.ContainsKey(c)) {
                return BlockEnum.NormalBlock;
            }
            return blockTypeDictionary[c];
        }
        //public char[] BlockChars(String[] legend) {
        //    var arr = new char[legend.Length];
        //    for (int i = 0; i < legend.Length; i++){
        //        if (!string.IsNullOrEmpty(legend[i]))
        //            arr[i] = char.Parse(legend[i].Substring(0, 1));
        //    }
        //    return arr;
        //}
    }
}