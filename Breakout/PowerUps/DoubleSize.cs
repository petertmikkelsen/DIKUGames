using Breakout.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public class DoubleSize : PowerUp {
        public DoubleSize(Vec2F position) : base(position, "BigPowerUp.png") {
        }
        public override void ActivatePowerUp(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, Message = "DOUBLE_SIZE"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, Message = "HALF_SIZE"}, DIKUArcade.Timers.TimePeriod.NewSeconds(3.0));
        }
    }
}