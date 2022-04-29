using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Utilities;
using DIKUArcade.Events;

namespace Breakout.PowerUps {
    public class ExtraLife : PowerUp {
        public ExtraLife(Vec2F position) : base(position, "LifePickUp.png") {
        }
        public override void ActivatePowerUp(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent, Message = "EXTRA_LIFE"});
        }
    }
}