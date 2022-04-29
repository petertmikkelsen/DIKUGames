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

namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor {
        public Ball ball {private set; get;}
        public Player player {private set; get;}
        public EntityContainer<Block> blocks {private set; get;}
        private LevelCreator levelCreator;
        public CollisionControler collisionControler {private set; get;}
        private StateMachine stateMachine;
        


        public Game(WindowArgs windowArgs) : base (windowArgs) {
            window.SetKeyEventHandler(HandleKeyEvent);
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.0225f)), new Image(ImageDatabase.GetImageFilePath("Player.png")));
            ball = new Ball(new DynamicShape(new Vec2F(0.485f, 0.1225f), new Vec2F(0.03f, 0.03f), new Vec2F(0.006f, 0.009f)), 
                new Image(ImageDatabase.GetImageFilePath("ball.png")));
            blocks = new EntityContainer<Block>();
            levelCreator = new LevelCreator(blocks);

            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
           
            levelCreator.LoadNewlevel(Path.Combine("Assets", "Levels", "level1.txt"));
            collisionControler = new CollisionControler (blocks, ball, player);
            //stateMachine = new StateMachine(this);
            //BreakoutBus.GetBus().InitializeEventBus(new List<GameEventType>{
            //    GameEventType.GameStateEvent,
            //    GameEventType.InputEvent
            //});
        }

        private void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
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
                    case KeyboardKey.C:
                        // sender et event afsted af typen WindowEvent
                        Console.WriteLine("event sendt afsted");
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.WindowEvent, Message = "CHANGE_COLOR" });
                        // window.SetClearColor(System.Drawing.Color.Chocolate);
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
       
        public override void Render() {
            //stateMachine.Render();
            ball.Render();
            player.Render();
            blocks.RenderEntities();            

        }
        public override void Update() {
            player.Move();
            ball.Move();
            collisionControler.CollisionDetector();
            BreakoutBus.GetBus().ProcessEvents();
        }

        public void ProcessEvent(GameEvent gameEvent)
        {
            // if (gameEvent.Message == "CHANGE_COLOR") {
            //     Console.WriteLine("event modtaget");
            //     window.SetClearColor(System.Drawing.Color.Chocolate);
            // }
        }

    }
}