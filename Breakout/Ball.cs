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

        public Ball(DynamicShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        public void Render() {
            entity.RenderEntity();
        }
        public void Move() {

        }
    }
}