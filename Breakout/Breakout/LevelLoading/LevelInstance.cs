using System;
using System.IO;
using DIKUArcade.Math;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.LevelHandling;
using Breakout.Utilities;

namespace Breakout.LevelLoading {
    public class LevelInstance {

        public EntityContainer<Block> blocks { private set; get; }
        public Player player {private set; get;}


        public LevelInstance(LevelDefinition levelDefinition) {
            blocks = new EntityContainer<Block>();
            for (int i = 0; i < levelDefinition.map.Length; i ++) {
                for (int j = 0; j < levelDefinition.map[i].Length; j++) {
                    if (levelDefinition.map[i][j] != '-') {
                        string c = Char.ToString(levelDefinition.map[i][j]);
                        CreateBlock(new Vec2F(0.02f + j * 0.08f, 0.95f - 
                            i * 0.03f), levelDefinition.legendDictionary[c], levelDefinition.GetBlockEnum(c));
                    }                        
                }
            }
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.2f, 0.025f)), //Second vector makes the player wide and short.
                new Image(Path.Combine("Assets", "Images", "player.png")));
        }
        private void CreateBlock (Vec2F position, string filename, BlockEnum block) {
            var shape = new StationaryShape(position, new Vec2F(0.08f, 0.03f));
            var image = ImageDatabase.GetInstance().GetImage(filename); 
            if (block == BlockEnum.Default) {
                blocks.AddEntity(new DefaultBlock(shape, image));
            }
        }
        public void Render() {
            blocks.RenderEntities();
            player.RenderEntity();
        }
        public void Update() {
            player.Move();
        }
    }
}