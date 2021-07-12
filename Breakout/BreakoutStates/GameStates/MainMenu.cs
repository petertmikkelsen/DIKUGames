using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;


namespace Breakout {

    /// <summary>
    /// A game state for the main menu. From this state one can either start a new game
    /// or quit the game.
    /// </summary>
    public class MainMenu : IGameState {
        private Entity background;
        private Text startGame;
        private Text exitGame;

        public MainMenu() {
            background = new Entity(Constants.BackGroundShape, 
                new Image(Path.Combine("../", "Breakout","Assets", "Images", "BreakoutTitleScreen.png")));
            
            startGame = new Text("Press Space \nto start", new Vec2F(0.35f, 0.4f), new Vec2F(0.3f, 0.3f));
            startGame.SetColor(255, 255, 255, 255); 

            exitGame = new Text("Press Escape \nto exit", new Vec2F(0.35f, 0.2f), new Vec2F(0.3f, 0.3f));
            exitGame.SetColor(255, 255, 255, 255);
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                if (key == KeyboardKey.Space) {
                    StateMachine.GetStateMachine().GetGameState(GameStateType.GameRunning).ResetState();
                    StateMachine.GetStateMachine().SwitchState(GameStateType.GameRunning);
                    //BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    //    EventType = GameEventType.GameStateEvent, Message = "GAME_RUNNING"}); 
                } else if (key == KeyboardKey.Escape) {
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                        EventType = GameEventType.WindowEvent, Message = "CLOSE_WINDOW"});
                }
            }
        }

        public void RenderState()
        {
            background.RenderEntity();
            startGame.RenderText();
            exitGame.RenderText();
        }

        public void ResetState()
        {

        }

        public void UpdateState()
        {

        }
    }
}