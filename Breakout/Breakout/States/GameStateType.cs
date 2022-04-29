using System;
namespace Breakout{
    public enum GameStateType {
        GamePaused,
        GameRunning,
        MainMenu
    }
    public class StateTransformer {
        public GameStateType TransformStringToState (string state) { //Adapter pattern
            switch (state) {
                case "GAME_PAUSED":
                    return GameStateType.GamePaused;
                case "GAME_RUNNING":
                    return GameStateType.GameRunning;
                case "MAIN_MENU":
                    return GameStateType.MainMenu;
                default:
                    throw new ArgumentException(String.Format("{0} is not a valid state", state));
            }
        }
        
        public string TransformStateToString (GameStateType state) { //Adapter pattern
            switch (state) {
                case GameStateType.GamePaused:
                    return "GAME_PAUSED";
                case GameStateType.GameRunning:
                    return "GAME_RUNNING";
                case GameStateType.MainMenu:
                    return "MAIN_MENU";
                default:
                    throw new  ArgumentException(String.Format("{0} is not a valid state", state));
            }
        }
    }
}
