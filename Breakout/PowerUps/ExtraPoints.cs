using Breakout.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.PowerUps {
    public class ExtraPoints : PowerUp {
        public ExtraPoints(Vec2F position) : base(position, "PointImage.png") {
        }
        public override void ActivatePowerUp(){
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.GameStateEvent, Message = "EXTRA_POINTS"});
        }
    }
}