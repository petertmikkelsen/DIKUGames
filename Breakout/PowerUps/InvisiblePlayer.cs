using DIKUArcade.Events;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public class InvisiblePlayer : PowerUp{
        public InvisiblePlayer(Vec2F position) : base(position, "DoubleSpeedPowerUp.png") {
        }
        public override void ActivatePowerUp(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent, Message = "INVISIBLE_PLAYER"});
            BreakoutBus.GetBus().RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, Message = "VISIBLE_PLAYER"}, DIKUArcade.Timers.TimePeriod.NewSeconds(2.0));
        }
    }
}