using Breakout.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public abstract class PowerUp : Entity{
        public PowerUp(Vec2F position, string image) : base(PosToShape(position), ImageDatabase.GetInstance().GetImage(image)) {
        }
        public static DynamicShape PosToShape(Vec2F position) {
            return new DynamicShape(position - new Vec2F(0.03f, 0.03f), new Vec2F(0.06f, 0.06f), new Vec2F(0.0f, -0.005f));
        }
        public void Move() {
            if (Shape.Position.Y <= 0.0f) {
                DeleteEntity();
            }
            else {
                Shape.Move();
            }
        }
        public abstract void ActivatePowerUp();

    }
}