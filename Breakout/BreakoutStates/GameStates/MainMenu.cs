using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;


namespace Breakout.States {

    /// <summary>
    /// A game state for the main menu. From this state one can either start a new game
    /// or quit the game.
    /// </summary>
    public class MainMenu : IGameState
    {
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            throw new System.NotImplementedException();
        }

        public void RenderState()
        {
            throw new System.NotImplementedException();
        }

        public void ResetState()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateState()
        {
            throw new System.NotImplementedException();
        }
    }
}