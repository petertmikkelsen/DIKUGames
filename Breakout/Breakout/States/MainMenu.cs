using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using System.IO;
using System;
using DIKUArcade.Events;
using System.Collections.Generic;


namespace Breakout.States {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;        
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        private bool btnNum = true;
        public static MainMenu GetInstance() {
            if (MainMenu.instance == null) {
                instance = new MainMenu();
                MainMenu.instance.InitializeGameState();
            }
            return MainMenu.instance;
        }
        public void InitializeGameState() { //Setting up the main menu screen
            menuButtons = new Text[2]; //Two buttons. Singleton
            backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png")));
            menuButtons[0] = new Text("New Game", new Vec2F(0.2f, 0.4f), new Vec2F(0.3f, 0.3f)); //First button is "New Game"
            menuButtons[0].SetColor(System.Drawing.Color.White); //The text is initially white
            menuButtons[1] = new Text("Quit", new Vec2F(0.2f, 0.2f), new Vec2F(0.3f, 0.3f));
            menuButtons[1].SetColor(System.Drawing.Color.White);
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key){
            // if (action == KeyboardAction.KeyPress) {
            //     switch (key) {
            //         default:
            //             break;
            //     }
            // }
            if (action == KeyboardAction.KeyRelease) {
                switch (key) {
                    case KeyboardKey.Up:
                        if ( !btnNum ) //Two buttons = binary. If the button is already highlighted, switch to the other upon up/down.
                            btnNum = true;
                        break;
                    case KeyboardKey.Down:
                        if ( btnNum )
                            btnNum = false;
                        break;
                    case KeyboardKey.Enter:
                        if (btnNum) { //if button 0 (start game) is highlighted, and enter is clicked, then update the EventType to start the game.
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                                EventType = DIKUArcade.Events.GameEventType.GameStateEvent, Message="START_GAME"}); 
                            //StateMachine.GetStateMachine().SwitchState(GameStateType.GameRunning);
                        } else {
                            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                                EventType = DIKUArcade.Events.GameEventType.GameStateEvent, Message="QUIT"}); 
                        }
                        break;
                    case KeyboardKey.Escape:
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = DIKUArcade.Events.GameEventType.GameStateEvent, Message="QUIT"});
                        break;
                    default:
                        break;
                }
            }
        }
        public void UpdateCursor() {
            if (btnNum) {
                menuButtons[0].SetColor(System.Drawing.Color.Red); //The selected button-text is red, the other is white
                menuButtons[1].SetColor(System.Drawing.Color.White);
            }
            else {
                menuButtons[0].SetColor(System.Drawing.Color.White);
                menuButtons[1].SetColor(System.Drawing.Color.Red);
            }
        }

        public void RenderState() {//Load the buttons and the background image.
            backGroundImage.RenderEntity();
            menuButtons[0].RenderText();
            menuButtons[1].RenderText();
        }

        public void ResetState(){} //IGameState requires this.

        public void UpdateState(){
            UpdateCursor(); //When a different button is highlighted, this updates that.
        }
    }
}