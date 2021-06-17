using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using Breakout;


namespace Breakout {

    /// <summary>
    /// A game state for when the game is paused. From this state one should be
    /// able to choose between unpausin or enter the main menu state.
    /// </summary>
    public class GamePaused : IGameState
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