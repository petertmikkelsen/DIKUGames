using System;
using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using DIKUArcade.Timers;

namespace Breakout {
    /// <summary>
    /// A game state for when the game is paused. From this state one should be
    /// able to choose between unpausin or enter the main menu state.
    /// </summary>
    public class GamePaused : IGameState {   
        private Entity background;
        private Text ResumeGame;
        private Text MainMenu;

        public GamePaused() {
            background = new Entity(Constants.BackGroundShape, 
                new Image(Path.Combine("../", "Breakout","Assets", "Images", "BreakoutTitleScreen.png")));
            
            ResumeGame = new Text("Press P \nto resume", new Vec2F(0.4f, 0.4f), new Vec2F(0.3f, 0.3f));
            ResumeGame.SetColor(255, 255, 255, 255); 

            MainMenu = new Text("Press M \nto Menu", new Vec2F(0.4f, 0.2f), new Vec2F(0.3f, 0.3f));
            MainMenu.SetColor(255, 255, 255, 255);
        }
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                        break;
                    case KeyboardKey.P:
                        StateMachine.GetStateMachine().SwitchState(GameStateType.GameRunning);
                        break;
                    case KeyboardKey.M:
                        StateMachine.GetStateMachine().SwitchState(GameStateType.MainMenu);
                        break;
                    default:
                        break;
                }
            }
        }

        public void RenderState()
        {
            background.RenderEntity();
            ResumeGame.RenderText();
            MainMenu.RenderText();
        }

        public void ResetState()
        {
            
        }

        public void UpdateState()
        {
            
        }
    }
}