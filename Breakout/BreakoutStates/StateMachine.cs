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

namespace Breakout.BreakoutStates {
    public class StateMachine : IGameEventProcessor {
        public IGameState ActiveState { get; private set;}
        private IGameState MainMenu;
        private IGameState GameIsRunning;
        private IGameState GameIsPaused;

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            //MainMenu = new MainMenu();

            // ActiveState = ;
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                // case :
                //     ActiveState = stateType;
                //     break;
                default:
                    break; 
            }
        }

        public void ProcessEvent(GameEvent gameEvent)
        {
            throw new NotImplementedException();
        }
    }


}