using System;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;


namespace Breakout
{
    public class Ball : Entity
    {
        private Entity entity;
        private DynamicShape shape;
        // private const float MOVEMENT_SPEED = 0.02f;

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        public void Render() {
            entity.RenderEntity();
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
                Console.WriteLine("GAME OVER");
            }
            else
                shape.Move();

            shape.Move();

        }
    }
}