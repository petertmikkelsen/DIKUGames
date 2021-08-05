using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.Utilities;
using DIKUArcade.State;
using DIKUArcade.Input;
using BreakoutStates.GameStates;

namespace Breakout {
    public class StateMachine : IGameEventProcessor, IGameState {
        private static StateMachine stateMachine;
        public StateMachine(IGameState activeState) 
        {
            this.ActiveState = activeState;
               
        }
                public IGameState ActiveState { get; private set; }
        public List<GameEvent> eventQueueBuffer {private set; get;}
        private IGameState MainMenu;
        private IGameState GameRunning;
        private IGameState GamePaused;
        private IGameState GameOver;
        private IGameState GameCompleted;

        private StateMachine() {
            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
            // BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            eventQueueBuffer = new List<GameEvent>();
            MainMenu = new MainMenu();
            GameRunning = new GameRunning();
            GamePaused = new GamePaused();
            GameOver = new GameOver();
            GameCompleted = new GameCompleted();

            ActiveState = MainMenu;
            stateMachine = this;
        }

        public IGameState GetGameState(GameStateType gameStateType) {
            IGameState gameState = null;
            switch (gameStateType) {
                case GameStateType.MainMenu:
                    gameState = MainMenu;
                    break;
                case GameStateType.GameRunning:
                    gameState = GameRunning;
                    break;
                case GameStateType.GamePaused:
                    gameState = GamePaused;
                    break;
                case GameStateType.GameOver:
                    gameState = GameOver;
                    break;
                case GameStateType.GameCompleted:
                    gameState = GameCompleted;
                    break;
                default:
                    break;
            }
            return gameState;
        }

        public static StateMachine GetStateMachine() {
            return stateMachine ?? (stateMachine = new StateMachine());
        }

        public void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning;
                        break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused;
                        break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu;
                        break;
                case GameStateType.GameOver:
                    ActiveState = GameOver;
                        break;
                case GameStateType.GameCompleted:
                    ActiveState = GameCompleted;
                        break;
                default:
                    break;
            }
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.WindowEvent, Message = "STATE_CHANGE"});
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "SWITCH_STATE") {
                switch (gameEvent.StringArg1) {
                    case "MAIN_MENU":
                        SwitchState(GameStateType.MainMenu);
                        break;
                    case "GAME_RUNNING":
                        SwitchState(GameStateType.GameRunning);
                        break;
                    case "GAME_PASUED":
                        SwitchState(GameStateType.GamePaused);
                        break;
                    case "GAME_OVER":
                        SwitchState(GameStateType.GameOver);
                        break;
                    case "GAME_COMPLETED":
                        SwitchState(GameStateType.GameCompleted);
                        break;
                }
            }
        }
        public void QueueEvent(GameEvent gameEvent) {
            eventQueueBuffer.Add(gameEvent);
        }

        public void ResetState() {
        }

        public void UpdateState() {
            ActiveState.UpdateState();
            foreach (var events in eventQueueBuffer) {
                BreakoutBus.GetBus().RegisterEvent(events);
            }
            eventQueueBuffer.Clear();
        }

        public void RenderState() {
            ActiveState.RenderState();
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            ActiveState.HandleKeyEvent(action, key);
        }
    }


}