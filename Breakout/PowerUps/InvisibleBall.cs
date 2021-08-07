using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout.PowerUps {
    public class InvisibleBall : PowerUp{
        public InvisibleBall(Vec2F position) : base(position, "DoubleSpeedPowerUp.png") {
        }
        public override void ActivatePowerUp(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent, Message = "INVISIBLE_BALL"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, Message = "VISIBLE_BALL"}, DIKUArcade.Timers.TimePeriod.NewSeconds(2.0));
        }
    }
}