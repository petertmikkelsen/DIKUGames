using System;
using System.Collections.Generic;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

namespace Breakout.Entities
{
    public class Ball : Entity, IGameEventProcessor, IBall {
        public float speedModifier = 1;
        public int invisCounter;
        public Ball(DynamicShape xShape, IBaseImage image) : base(xShape, image) {
            Shape = Shape.AsDynamicShape();
            //speed = MathF.Sqrt(MathF.Pow(this.Shape.Direction.X, 2.0f)+MathF.Pow(this.Shape.Direction.Y, 2.0f));
            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);

        }
        public void Move(float speed) {
            
            if (speed < 0.00001f) {
                return;
            }
            GetShape().Direction *= speed / (float)GetShape().Direction.Length();
            if (GetShape().Position.X >= 1.0f - GetShape().Extent.X){
                GetShape().Direction.X = (GetShape().Direction.X * (-1));
                GetShape().Position.X = 1.0f - GetShape().Extent.X - 0.000001f;
                GetShape().Move();
            }
            else if (GetShape().Position.X <= 0.0f){
                GetShape().Direction.X = (GetShape().Direction.X * (-1));
                GetShape().Position.X = 0.000001f;
                GetShape().Move();
            }
            else if (GetShape().Position.Y >= 1.0f - GetShape().Extent.Y) {
                GetShape().Direction.Y = (GetShape().Direction.Y * (-1));
                GetShape().Position.Y = 1.0f - GetShape().Extent.Y - 0.000001f;
                GetShape().Move();
            }
            else if (GetShape().Position.Y <= 0.0f) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent, Message = "BALL_DEAD"});
            }
            else
                GetShape().Move();
        }
        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }
        public void SetPositionBlockHitUnderOrAbove() {
            GetShape().Direction.Y = (GetShape().Direction.Y * (-1));
        }
        public void SetPositionBlockHitSide() {
            GetShape().Direction.X = (GetShape().Direction.X * (-1));
        }
        public void SetPositionPlayerHit(float x, float currentSpeed) {
            GetShape().Direction.X = x;
            float y = MathF.Sqrt(MathF.Pow(currentSpeed, 2.0f)-MathF.Pow(x,2.0f));
            GetShape().Direction.Y = y;
        }
        public void Destroy() {
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.GameStateEvent, this);
            DeleteEntity();
        }
        public float Speed() {
            return speedModifier * Constants.BallVelocity;
        }
        public Vec2F GetPosition() {
            return GetShape().Position;
        }
        public void Render() {
            RenderEntity();
        }
        public void Update() {
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "DOUBLE_SIZE") {
                GetShape().Extent = GetShape().Extent * 2;
            }
            if (gameEvent.Message == "HALF_SIZE") {
                GetShape().Extent = GetShape().Extent / 2;
            }
            if (gameEvent.Message == "DOUBLE_SPEED") {
                speedModifier *= 2;
            }
            if (gameEvent.Message == "HALF_SPEED") {
                speedModifier /= 2;
            }
            if (gameEvent.Message == "INVISIBLE_BALL") {
                invisCounter++;
                if (invisCounter != 0) {
                    isVisible = false;
                }
            }
            if (gameEvent.Message == "VISIBLE_BALL") {
                invisCounter--;
                if (invisCounter == 0) {
                    isVisible = true;
                }
            }
        }
    }
}