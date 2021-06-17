using System.IO;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;


namespace Breakout.States {

    /// <summary>
    /// A game state for when the game is running.
    /// </summary>
    public class GameRunning : IGameState, IGameEventProcessor
    {
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key)
        {
            throw new System.NotImplementedException();
        }

        public void ProcessEvent(GameEvent gameEvent)
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