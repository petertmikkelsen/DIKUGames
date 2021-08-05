using System;
using System.Drawing;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Utilities;
using DIKUArcade.Graphics;
using Breakout.Blocks;
using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout.LevelHandling {
    public class Level : IGameEventProcessor {
        public EntityContainer<Block> blocks { private set; get; }
        public bool hasStarted;
        public long startTime;
        public long levelTime;
        public Level(LevelDefinition levelDefinition) {
            blocks = new EntityContainer<Block>();
            for (int i = 0; i < levelDefinition.map.Length; i ++) {
                for (int j = 0; j < levelDefinition.map[i].Length; j++) {
                    if (levelDefinition.map[i][j] != '-') {
                        string c = Char.ToString(levelDefinition.map[i][j]);
                        CreateBlock(new Vec2F(0.02f + j * 0.08f, 0.95f - 
                            i * 0.03f), levelDefinition.legendDictionary[c]);
                    }                        
                }
            }
            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
            BusBuffer.GetBuffer().Subscribe(GameEventType.MovementEvent, this);
            levelTime = 10000;
        }
        private void CreateBlock (Vec2F position, string image) {
            if (image == "darkgreen-block.png" ) {
                blocks.AddEntity(new HardenedBlock(
                    new StationaryShape(position, new Vec2F(0.08f, 0.03f)),
                    new DIKUArcade.Graphics.Image(ImageDatabase.GetImageFilePath(image))));
            }
            else if (image == "grey-block.png") {
                blocks.AddEntity(new UnbreakableBlock(
                    new StationaryShape(position, new Vec2F(0.08f, 0.03f)),
                    new DIKUArcade.Graphics.Image(ImageDatabase.GetImageFilePath(image))));
            }
            else if (image == "green-block.png") {
                blocks.AddEntity(new ExplosiveBlock(
                    new StationaryShape(position, new Vec2F(0.08f, 0.03f)),
                    new DIKUArcade.Graphics.Image(ImageDatabase.GetImageFilePath(image))));
            }
            else {
                blocks.AddEntity(new NormalBlock(
                    new StationaryShape(position, new Vec2F(0.08f, 0.03f)),
                    new DIKUArcade.Graphics.Image(ImageDatabase.GetImageFilePath(image))));    
            }
        }
        public void Start() {
            if (hasStarted == false) {
                startTime = StaticTimer.GetElapsedMilliseconds();
                hasStarted = true;
            }
        }
        public void HasExpired() {
            if (hasStarted && StaticTimer.GetElapsedMilliseconds() - startTime > levelTime) {
                
            }
        }
        public void Destroy() {
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.GameStateEvent, this);
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.MovementEvent, this);
        }
        public void render() {
            blocks.RenderEntities();
        }
        public void update() {
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "BLOCK_DESTROYED" && blocks.CountEntities() == 0) {
                StateMachine.GetStateMachine().QueueEvent(new GameEvent{
                    EventType = GameEventType.GameStateEvent, Message = "LEVEL_COMPLETED"});
            }
            if (gameEvent.Message == "EXPLOSION") {
                foreach (Block block in blocks) {
                    if (gameEvent.ObjectArg1 != null) {
                        var position = gameEvent.ObjectArg1 as Vec2F;
                        if (position != null) {
                        var range = new Vec2F(0.1f, 0.05f);
                            if (Math.Abs(block.Shape.Position.X - position.X) <= range.X && Math.Abs(block.Shape.Position.Y - position.Y) <= range.Y)
                                block.TakeDamage();
                        }
                    }
                }
            }
        } 
    }
}