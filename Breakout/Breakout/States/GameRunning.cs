using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using Breakout.LevelLoading;

namespace Breakout.BreakoutStates {
    public class GameRunning : IGameState, IGameEventProcessor{
        private static GameRunning instance = null;
        public GameInstance? gameInstance {private set; get;}

        private Entity backGroundImage;
        private Player player; //fjern på et tidspunkt
        private Score scoreBoard;
        private int startMovement = 0;
        private bool checkEnemy = true;
        public bool isDead {get; private set;} = false;
        public static GameRunning GetInstance() { //Singleton. Creates a new game instance if there are none. If there is one, the prior instance is used

            if (GameRunning.instance == null) {
                instance = new GameRunning();
                GameRunning.instance.InitializeGameState();
            }
            return GameRunning.instance;
       }
        public void InitializeGameState() { //When the state switches to GameRunning, a player will be created, and the GameRunning class subscribes to the bus
            
            BreakoutBus.GetBus().Subscribe(DIKUArcade.Events.GameEventType.InputEvent, this); //Gets informed about the InputEvents and GameStateEvents.
            BreakoutBus.GetBus().Subscribe(DIKUArcade.Events.GameEventType.GameStateEvent, this);
            
            gameInstance = new GameInstance();
            //scoreBoard = new Score(new Vec2F(0.85f, 0.75f), new Vec2F(0.25f, 0.25f));
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){ //Handles the inputs from the bus while this is the game state.
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Right:
                        System.Console.WriteLine("1");
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = DIKUArcade.Events.GameEventType.GameStateEvent, StringArg1="KEY_RIGHT", Message="KEY_PRESS"});
                        break;
                    case KeyboardKey.Left:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = DIKUArcade.Events.GameEventType.GameStateEvent, StringArg1="KEY_LEFT", Message="KEY_PRESS"});
                        break;
                    default:
                        break;
                }
            }
            if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Right:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = DIKUArcade.Events.GameEventType.GameStateEvent, StringArg1="KEY_RIGHT", Message="KEY_RELEASE"});
                        break;
                    case KeyboardKey.Left:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = DIKUArcade.Events.GameEventType.GameStateEvent, StringArg1="KEY_LEFT", Message="KEY_RELEASE"});
                        break;
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                            EventType = DIKUArcade.Events.GameEventType.GameStateEvent, Message="QUIT"});
                        break;
                    default:
                        break;
                }
            }
        }
        // public void GameOver() {
        //     isDead = true;
        //     //Skift state så GameOver ikke er en del af GameRunning
        //     gameOverText = new Text("Game Over!", new Vec2F(0.375f, 0.4f), new Vec2F(0.25f,0.25f));
        //     gameOverScore = new Text("Your score: "+ scoreBoard.score, new Vec2F(0.375f, 0.3f), new Vec2F(0.25f,0.25f));
        //     gameOverText.SetColor(System.Drawing.Color.White);
        //     gameOverScore.SetColor(System.Drawing.Color.White);
        // }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.EventType == GameEventType.InputEvent) {
                switch (gameEvent.Message) {
                     case "KEY_SPACE":
                        break;
                    case "GAME_OVER":
                        //GameOver();
                        break;
                }
            }   
        }
        public void RenderState() { //her skal der også ændres lidt
            gameInstance.render();
        }
        public void ResetState(){}
        public void UpdateState(){
            BreakoutBus.GetBus().ProcessEventsSequentially();
            gameInstance.Update();
            
        }
    }
}