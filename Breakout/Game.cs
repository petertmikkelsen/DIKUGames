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

namespace Breakout {
    public class Game : DIKUGame {
        public Ball ball;
        public Player player;
        private LevelCreator levelCreator;

        public Game(WindowArgs windowArgs) : base (windowArgs) {
            window.SetKeyEventHandler(HandleKeyEvent);
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.0225f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
            ball = new Ball(new DynamicShape(new Vec2F(0.485f, 0.1225f), new Vec2F(0.03f, 0.03f), new Vec2F(0.000f, 0.001f)), 
                new Image(Path.Combine("Assets", "Images", "ball.png")));
            levelCreator = new LevelCreator();
            levelCreator.LoadNewlevel(Path.Combine("Assets", "Levels", "level1.txt"));
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Left:
                        player.SetMoveLeft(true);
                            break;
                    case KeyboardKey.Right:
                        player.SetMoveRight(true);
                            break;
                    case KeyboardKey.Escape:
                        window.CloseWindow();
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
        public override void Render()
        {
            ball.Render();
            player.Render();
            levelCreator.blocks.RenderEntities();
        }
        public override void Update()
        {
            ball.Shape.Move();
            player.Move();
            ball.Move();
        }
    }
}