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
        public IGameState ActiveState { get; private set;}
        private IGameState MainMenu;
        private IGameState GameRunning;
        private IGameState GamePaused;

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            MainMenu = new MainMenu();
            GameRunning = new GameRunning();
            GamePaused = new GamePaused();

            ActiveState = MainMenu;
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameRunning:
                    ActiveState = GameRunning;
                    break;
                default:
                    break; 
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "MAIN_MENU") {
                System.Console.WriteLine("virker");
                SwitchState(GameStateType.MainMenu);
            }
        }

        public void ResetState()
        {
            throw new NotImplementedException();
        }

        public void UpdateState()
        {
            throw new NotImplementedException();
        }

        public void RenderState()
        {
            throw new NotImplementedException();
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            throw new NotImplementedException();
        }
    }


}