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

        public MainMenu() {
            background = new Entity(
                new StationaryShape(new Vec2F(1.0f, 1.0f), new Vec2F(1.0f, 1.0f)), 
                new Image(Path.Combine("../", "Breakout","Assets", "Images", "BreakoutTitleScreen.png")));
        }

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            throw new System.NotImplementedException();
        }

        public void RenderState()
        {
            background.RenderEntity();
        }

        public void ResetState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {

        }
    }
}