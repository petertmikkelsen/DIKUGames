using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    public class PowerUpBlock : Block{
        public PowerUpBlock(StationaryShape shape, IBaseImage image) : base (shape, image, 1, Blocks.BlockEnum.PowerUpBlock) {
            hitPoints = 1;
        }
        public override void TakeDamage()
        {
            base.TakeDamage();
            BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                EventType = GameEventType.MovementEvent, ObjectArg1 = Shape.Position, Message = "SPAWN_POWERUP"});
        }
    }
}