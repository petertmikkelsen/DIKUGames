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

namespace Breakout
{
    public class LevelCreator
    {
        LevelLoader levelLoader = new LevelLoader();
        public EntityContainer<Block> blocks;
        public string[] map { private set; get; }

        Dictionary<string, string> metaDictionary =
            new Dictionary<string, string>();
        Dictionary<string, string> legendDictionary =
            new Dictionary<string, string>();

        
        public LevelCreator(EntityContainer<Block> blocks){
            this.blocks = blocks;

        }
        
        // Creates a block and add it to the block list
        public void CreateBlock (Vec2F position, string image) {
            if (image == "darkgreen-block.png" ) {
                blocks.AddEntity(new HardenedBlock(
                    new StationaryShape(position, new Vec2F(0.08f, 0.03f)),
                    new Image(Path.Combine("Assets", "Images",  image))));
            }
            else {
                blocks.AddEntity(new NormalBlock(
                    new StationaryShape(position, new Vec2F(0.08f, 0.03f)),
                    new Image(Path.Combine("Assets", "Images",  image))));
            }
        } 

        // Calls CreateBlock for each block in map, with correct(?) position
        public void LevelBuilder () {
            for (int i = 0; i < map.Length; i ++) {
                for (int j = 0; j < map[i].Length; j++) {
                    if (map[i][j] != '-') {
                        string c = Char.ToString(map[i][j]);
                        CreateBlock(new Vec2F(0.02f + j * 0.08f, 0.95f - 
                            i * 0.03f), legendDictionary[c]);
                    }                        
                }
            }
        }

        // Calls the above functions in correct order
        public void LoadNewlevel(string levelAsASCII) {
            var input = levelLoader.GetSubfiles(levelAsASCII);
            map = input.Item1;
            metaDictionary = input.Item2;
            legendDictionary = input.Item3;

            LevelBuilder();
        }
    }
}