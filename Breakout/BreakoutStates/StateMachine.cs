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

namespace Breakout {
    public class StateMachine : IGameEventProcessor, IGameState {
        private static StateMachine stateMachine;
        public IGameState ActiveState { get; private set; }
        private IGameState MainMenu;
        private IGameState GameRunning;
        private IGameState GamePaused;

        private StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            // BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            MainMenu = new MainMenu();
            GameRunning = new GameRunning();
            GamePaused = new GamePaused();

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
                default:
                    break; 
            }
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
                }
            }
        }

        public void ResetState()
        {
        }

        public void UpdateState()
        {
            ActiveState.UpdateState();
        }

        public void RenderState()
        {
            ActiveState.RenderState();
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            ActiveState.HandleKeyEvent(action, key);
        }
    }


}