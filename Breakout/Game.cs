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

namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor {
        private StateMachine stateMachine;

        public Game(WindowArgs windowArgs) : base (windowArgs) {
            window.SetKeyEventHandler(HandleKeyEvent);

            stateMachine = new StateMachine();

            BreakoutBus.GetBus().InitializeEventBus(new List <GameEventType> {GameEventType.WindowEvent, GameEventType.GameStateEvent});

            BreakoutBus.GetBus().Subscribe(GameEventType.WindowEvent, this);

        }
        private void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            stateMachine.HandleKeyEvent(action, key);
        }

        // ælaksdjf
       
        public override void Render() {
            stateMachine.RenderState();         
        }
        public override void Update() {
            stateMachine.UpdateState();
            BreakoutBus.GetBus().ProcessEvents();
        }

        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.Message == "CLOSE_WINDOW")
                window.CloseWindow();
        }
    }
}