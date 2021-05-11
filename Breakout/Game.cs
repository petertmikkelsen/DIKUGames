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

namespace Breakout {
    public class Game : DIKUGame {
        public Player player;
        private LevelLoader levelLoader;
        public Game(WindowArgs windowArgs) : base (windowArgs) {
            window.SetKeyEventHandler(HandleKeyEvent);
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.0225f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
            levelLoader = new LevelLoader();
            levelLoader.LoadNewlevel(Path.Combine("Assets", "Levels", "level1.txt"));
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
            player.Render();
            levelLoader.blocks.RenderEntities();
        }
        public override void Update()
        {
            player.Move();
        }
    }
}