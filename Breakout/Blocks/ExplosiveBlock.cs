using System;
using DIKUArcade.Entities;
using Breakout.Utilities;
using DIKUArcade.Graphics;
using DIKUArcade.Events;

namespace Breakout.Blocks {
    public class ExplosiveBlock : Block{
        public ExplosiveBlock(StationaryShape shape, IBaseImage image) : base (shape, image, 5, Blocks.BlockEnum.ExplosiveBlock) {
            hitPoints = 1;
        }
        public override void TakeDamage() {
            base.TakeDamage();
            StateMachine.GetStateMachine().QueueEvent(new GameEvent{
                EventType = GameEventType.MovementEvent, ObjectArg1 = Shape.Position, Message = "EXPLOSION"});
        }
    }
}