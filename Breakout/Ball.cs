using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;


namespace Breakout
{
    public class Ball : Entity
    {
        private Entity entity;
        public DynamicShape shape;
        public float speed;
        // private const float MOVEMENT_SPEED = 0.02f;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            speed = MathF.Sqrt(MathF.Pow(this.shape.Direction.X, 2.0f)+MathF.Pow(this.shape.Direction.Y, 2.0f));
        }
        
        public void Move() {
            if (shape.Position.X >= 0.97f){
                // SetMoveRight(false);
                shape.Direction.X = (shape.Direction.X * (-1));
                shape.Move();
            }
            else if (shape.Position.X <= 0.0f){
                // SetMoveLeft(false);
                shape.Direction.X = (shape.Direction.X * (-1));
                shape.Move();
            }
            else if (shape.Position.Y >= 0.97f) {
                shape.Direction.Y = (shape.Direction.Y * (-1));
                shape.Move();
            }
            else if (shape.Position.Y <= 0.0f) {
                entity.DeleteEntity();
                // Console.WriteLine("GAME OVER");
            }
            else
                shape.Move();

            // shape.Move();

        }

        public void SetPositionBlockHitUnderOrAbove() {
            shape.Direction.Y = (shape.Direction.Y * (-1));
        }
        public void SetPositionBlockHitSide() {
            shape.Direction.X = (shape.Direction.X * (-1));
        }
        public void SetPositionPlayerHit(float x) {

            shape.Direction.X = x;
            float y = MathF.Sqrt(MathF.Pow(this.speed, 2.0f)+MathF.Pow(x,2.0f));
            shape.Direction.Y = y;
        }
        
        public Vec2F GetPosition() {
            return shape.Position;
        }
        public void Render() {
            entity.RenderEntity();
        }
    }
}