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

        public Game(WindowArgs windowArgs) : base (windowArgs) {
            
            window.SetKeyEventHandler(HandleKeyEvent);
            window.SetClearColor(System.Drawing.Color.SkyBlue);
            
            ImageDatabase.GetInstance().initialize();

            BreakoutBus.GetBus().InitializeEventBus(new List <GameEventType> {GameEventType.WindowEvent, GameEventType.GameStateEvent, GameEventType.MovementEvent});

            BusBuffer.GetBuffer().Subscribe(GameEventType.WindowEvent, this);
        }
        
        private void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            StateMachine.GetStateMachine().HandleKeyEvent(action, key);
        }
       
        public override void Render() {
            StateMachine.GetStateMachine().RenderState();         
        }
        public override void Update() {
            StateMachine.GetStateMachine().UpdateState();
            BreakoutBus.GetBus().ProcessEvents();
            BusBuffer.GetBuffer().update();
        }
        public void ProcessEvent(GameEvent gameEvent){
            if (gameEvent.Message == "CLOSE_WINDOW")
                window.CloseWindow();
        }
    }
}