using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System.Security.Principal;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using System;
using Breakout.Utilities;
using Breakout.BreakoutStates;


namespace Breakout {
    class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;
        private Player player;
        public Game(WindowArgs windowArgs) : base(windowArgs) {

            eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.GameStateEvent });
            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);

            ImageDatabase.GetInstance().initialize();

        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key){
            StateMachine.GetStateMachine().ActiveState.HandleKeyEvent(action, key);
        }

        public override void Render() {
            StateMachine.GetStateMachine().RenderState();     
        }

        public override void Update() {
            StateMachine.GetStateMachine().UpdateState();
        }  
        public void ProcessEvent(GameEvent gameEvent) {
        }      
    }
}