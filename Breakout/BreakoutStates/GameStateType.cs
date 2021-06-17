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

namespace Breakout
{
    public enum GameStateType {
        GameIsRunning,
        GameIsPaused,
        MainMenu 
    }
}
    //public class StateTransformer{
    //    public string GameStateString;
    //    public GameStateType CurrentState;
    //    public static GameStateType TransformStringToState(string state) {
    //    GameEventType CurrentState = new GameEventType();
    //    //CurrentState = (GameEventType)GameStateType.GameIsRunning;
    //        switch (state) {
    //            case "GAME_RUNNING":
    //                CurrentState = (GameEventType)GameStateType.GameIsRunning;
    //                break;
    //            case "GAME_PAUSED":
    //                CurrentState = (GameEventType)GameStateType.GameIsPaused;
    //                break;
    //            case "MAIN_MENU":
    //                CurrentState = (GameEventType)GameStateType.MainMenu;
    //                break;
    //            default:
    //                throw new ArgumentException();
    //        }
    //    }
//
    //    public string TransformStateToString(GameStateType state) {
    //        switch (state) {
    //            case (GameStateType.GameIsRunning):
    //                GameStateString = "GAME_RUNNING";
    //                break;
    //            case (GameStateType.GameIsPaused):
    //                GameStateString = "GAME_PAUSED";
    //                break;
    //            case (GameStateType.MainMenu):
    //                GameStateString = "MAIN_MENU";
    //                break; 
    //            default:
    //                throw new ArgumentException();
//
    //        }
        //}

        //}
    //}