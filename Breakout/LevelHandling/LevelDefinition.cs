using System.Collections.Generic;

namespace Breakout.LevelHandling
{
    public class LevelDefinition {
        public string[] map;
        public Dictionary<string, string> metaDictionary;
        public Dictionary<string, string> legendDictionary;
        public LevelDefinition(string[] map, Dictionary<string, string> metaDictionary, Dictionary<string, string> legendDictionary) {
            this.map = map;
            this.metaDictionary = metaDictionary;
            this.legendDictionary = legendDictionary;
        }
    }
}