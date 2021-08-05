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
using Breakout.LevelHandling;

namespace Breakout {

    /// <summary>
    /// A game state for when the game is running.
    /// </summary>
    public class GameRunning : IGameState, IGameEventProcessor {
        
        public GameInstance gameInstance {private set; get;}
        
        public GameRunning() {
            gameInstance = new GameInstance();
            
            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (GameKeyEvent(action, key)) 
                return;
            if (gameInstance.GameKeyEvent(action, key))
                return;
        }

        public bool GameKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Escape:
                        StateMachine.GetStateMachine().QueueEvent(new GameEvent {
                            EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                        return true;
                    }
            }
            return false;     
        }

        public void RenderState() {
            if (gameInstance != null) 
                gameInstance.render();
        }
 
        public void ResetState() {
            StateMachine.GetStateMachine().QueueEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent, Message = "START_GAME"});
        }
        public void UpdateState() {
            if (gameInstance != null)
                gameInstance.update();
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "START_GAME") {
                if (gameInstance != null)
                    gameInstance.Destroy();
                gameInstance = new GameInstance();
            }
            if (gameEvent.Message == "GAME_OVER") {
                StateMachine.GetStateMachine().SwitchState(GameStateType.GameOver);
            }
        }
    }
}