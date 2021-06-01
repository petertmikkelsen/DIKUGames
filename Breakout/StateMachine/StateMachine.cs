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
using DIKUArcade.State;

namespace Breakout
{
    public class StateMachine //: IGameEventProcessor
    {
        private GameStates gameState;
        public IGameState ActiveState;
        public StateMachine() {
            // BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent);
            gameState = GameStates.GameIsRunning;

        }

        // public void Render() {
        //     if (gameState == GameStates.MainMenu)  {
        //         //kode for menu
        //                  }
        //     else if (gameState == GameStates.GameIsRunning) {
// 
        //     }
// 
        //     else {
        //         // kode for pause
        //     }
        // }
        public void Update () {
                
        }

        //public void ProcessEvent(GameEvent gameEvent) {
        //    switch (gameEvent.Message) {
        //        case "GAME_OVER":
        //            System.Console.WriteLine("You are dead");
        //    }
        //    }
//
        //}



    }
}