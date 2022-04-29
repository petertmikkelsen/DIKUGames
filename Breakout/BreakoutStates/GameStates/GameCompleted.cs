using System.IO;
using System;
using Breakout;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using Breakout.Utilities;

namespace BreakoutStates.GameStates {
    public class GameCompleted : IGameState{
        private Entity background;
        private Text gameCompletedText;
        private Text backToMenu;
        public GameCompleted() {
            background = new Entity(Constants.BackGroundShape, 
                ImageDatabase.GetInstance().GetImage("shipit_titlescreen.png"));
            gameCompletedText = new Text("Congratulations! \n you finished", new Vec2F(0.1f, 0.3f), new Vec2F(0.6f, 0.3f));
            gameCompletedText.SetColor(System.Drawing.Color.Azure);
            backToMenu = new Text("Main Menu \n press m", new Vec2F(0.2f, 0.1f), new Vec2F(0.8f, 0.4f));
            backToMenu.SetColor(System.Drawing.Color.Azure);
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                if (key == KeyboardKey.M) {
                    StateMachine.GetStateMachine().SwitchState(GameStateType.MainMenu);
                } else if (key == KeyboardKey.Escape) {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                }
            }
        }
        public void ResetState() {

        }
        public void UpdateState() {
            
        }
        public void RenderState() {
            background.RenderEntity();
            gameCompletedText.RenderText();
        }
    }
}