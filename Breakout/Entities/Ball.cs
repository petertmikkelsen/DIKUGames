using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

namespace Breakout
{
    public class Ball : Entity, IGameEventProcessor{
        private Entity entity;
        public DynamicShape shape;
        public float accelerationT0;
        public float accelerationT1;
        public float speed0;
        public float speed1;
        public float speedModifier = 1;
        public int invisCounter;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            //speed = MathF.Sqrt(MathF.Pow(this.shape.Direction.X, 2.0f)+MathF.Pow(this.shape.Direction.Y, 2.0f));
            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
            accelerationT0 = 5000 + StaticTimer.GetElapsedMilliseconds();
            accelerationT1 = accelerationT0 + 30000;
            speed0 = 1;
            speed1 = 2;
        }
        public float Speed() {
            long t = StaticTimer.GetElapsedMilliseconds();
            float x = speed0 + (speed1 - speed0) * Math.Clamp((t - accelerationT0) / (accelerationT1 - accelerationT0), 0, 1);
            return (x * speedModifier) * Constants.BallVelocity;
        }
        public void Move() {
            if (shape.Position.X >= 1.0f - shape.Extent.X){
                shape.Direction.X = (shape.Direction.X * (-1));
                shape.Position.X = 1.0f - shape.Extent.X - 0.000001f;
                shape.Move();
            }
            else if (shape.Position.X <= 0.0f){
                shape.Direction.X = (shape.Direction.X * (-1));
                shape.Position.X = 0.000001f;
                shape.Move();
            }
            else if (shape.Position.Y >= 1.0f - shape.Extent.Y) {
                shape.Direction.Y = (shape.Direction.Y * (-1));
                shape.Position.Y = 1.0f - shape.Extent.Y - 0.000001f;
                shape.Move();
            }
            else if (shape.Position.Y <= 0.0f) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent, Message = "BALL_DEAD"});
            }
            else
                shape.Move();
        }

        public void SetPositionBlockHitUnderOrAbove() {
            shape.Direction.Y = (shape.Direction.Y * (-1));
        }
        public void SetPositionBlockHitSide() {
            shape.Direction.X = (shape.Direction.X * (-1));
        }
        public void SetPositionPlayerHit(float x) {
            shape.Direction.X = x;
            float y = MathF.Sqrt(MathF.Pow(Speed(), 2.0f)-MathF.Pow(x,2.0f));
            shape.Direction.Y = y;
        }
        public void Destroy() {
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.GameStateEvent, this);
        }
        public Vec2F GetPosition() {
            return shape.Position;
        }
        public void Render() {
            entity.RenderEntity();
        }
        public void Update() {
            shape.Direction /= (float)shape.Direction.Length();
            shape.Direction *= Speed();
            Move();
        }
        public void ResetPosition() {
            shape.Position = Constants.BallStartPosition;
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "DOUBLE_SIZE") {
                shape.Extent = shape.Extent * 2;
            }
            if (gameEvent.Message == "HALF_SIZE") {
                shape.Extent = shape.Extent / 2;
            }
            if (gameEvent.Message == "DOUBLE_SPEED") {
                speedModifier *= 2;
            }
            if (gameEvent.Message == "HALF_SPEED") {
                speedModifier /= 2;
            }
            if (gameEvent.Message == "INVISIBLE_BALL") {
                if (invisCounter != 0) {
                    isVisible = false;
                }
                invisCounter++;
            }
            if (gameEvent.Message == "VISIBLE_BALL") {
                isVisible = true;
                invisCounter--;
            }
        }
    }
}