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
using DIKUArcade.Input;
using DIKUArcade.Physics;
using Breakout.Utilities;
using Breakout;
using DIKUArcade.State;

namespace Breakout {

    /// <summary>
    /// A game state for when the game is running.
    /// </summary>
    public class GameRunning : IGameState, IGameEventProcessor
    {
        public Ball ball {private set; get;}
        public Player player {private set; get;}
        public EntityContainer<Block> blocks {private set; get;}
        private LevelCreator levelCreator;
        public CollisionControler collisionControler {private set; get;}

        public GameRunning() {
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.0225f)), new Image(ImageDatabase.GetImageFilePath("Player.png")));
            ball = new Ball(new DynamicShape(new Vec2F(0.485f, 0.1225f), new Vec2F(0.03f, 0.03f), new Vec2F(0.006f, 0.009f)), 
                new Image(ImageDatabase.GetImageFilePath("ball.png")));
            blocks = new EntityContainer<Block>();
            levelCreator = new LevelCreator(blocks);

            levelCreator.LoadNewlevel(Path.Combine("Assets", "Levels", "level1.txt"));
            collisionControler = new CollisionControler (blocks, ball, player);
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                        break;
                    case KeyboardKey.Left:
                        player.SetMoveLeft(true);
                            break;
                    case KeyboardKey.Right:
                        player.SetMoveRight(true);
                            break;
                    default:
                        break;
                }
            }
            else if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Left:
                        player.SetMoveLeft(false);
                            break;
                    case KeyboardKey.Right:
                        player.SetMoveRight(false);
                            break;
                    default:
                        break;
                }
            }
        }

        public void ProcessEvent(GameEvent gameEvent)
        {

        }



        public void RenderState() {
            ball.Render();
            player.Render();
            blocks.RenderEntities();  
        }

        public void ResetState() {}

        public void UpdateState() {
            player.Move();
            ball.Move();
            collisionControler.CollisionDetector();
        }
    }
}