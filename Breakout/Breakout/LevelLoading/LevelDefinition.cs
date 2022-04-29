using System;
using System.Collections.Generic;
using Breakout.Blocks;

namespace Breakout.LevelHandling
{
    public class LevelDefinition {
        public string[] map;
        public Dictionary<string, string> metaDictionary;
        public Dictionary<string, string> legendDictionary;
        public Dictionary<string, BlockEnum> BlockEnumDictionary;
        public LevelDefinition(string[] map, Dictionary<string, string> metaDictionary, Dictionary<string, string> legendDictionary) {
            this.map = map;
            this.metaDictionary = metaDictionary;
            this.legendDictionary = legendDictionary;
            BlockEnumDictionary = new Dictionary<string, BlockEnum>();
            //InitializeBlockDictionary();
        }
        private void InitializeBlockDictionary() {
        }
        public BlockEnum GetBlockEnum(string c) {
            if (!BlockEnumDictionary.ContainsKey(c)) {
                return BlockEnum.Default;
            }
            return BlockEnumDictionary[c];
        }
    }
}