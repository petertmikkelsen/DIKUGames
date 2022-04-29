using Breakout.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public class DoubleSpeed : PowerUp {
        public DoubleSpeed(Vec2F position) : base(position, "DoubleSpeedPowerUp.png") {
        }
        public override void ActivatePowerUp(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent, Message = "DOUBLE_SPEED"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, Message = "HALF_SPEED"}, DIKUArcade.Timers.TimePeriod.NewSeconds(3.0));
        }
    }
}