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

namespace BreakoutStates.GameStates
{
    public class GameOver : IGameState{


    private Entity background;
    private Text gameOverText;
    private Text backToMenu;

    public GameOver() {
        background = new Entity(Constants.BackGroundShape, 
                ImageDatabase.GetInstance().GetImage("shipit_titlescreen.png"));
        gameOverText = new Text("Game Over", new Vec2F(0.2f, 0.1f), new Vec2F(0.7f, 0.6f));
        gameOverText.SetColor(System.Drawing.Color.DarkRed);
        backToMenu = new Text("Main Menu \n press m", new Vec2F(0.2f, 0.1f), new Vec2F(0.8f, 0.4f));
        backToMenu.SetColor(System.Drawing.Color.DarkRed);
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
            gameOverText.RenderText();
            backToMenu.RenderText();
        }
    }
}