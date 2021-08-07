using Breakout.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public class HalfSpeed : PowerUp {
        public HalfSpeed(Vec2F position) : base(position,"HalfSpeedPowerUp.png") {
        }
        public override void ActivatePowerUp(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent, IntArg1 = 10000, Message = "HALF_SPEED"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, Message = "DOUBLE_SPEED"}, DIKUArcade.Timers.TimePeriod.NewSeconds(3.0));
        }
    }
}