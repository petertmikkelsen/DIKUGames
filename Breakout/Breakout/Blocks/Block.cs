using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    public abstract class Block : Entity {
        public int health {protected set; get;}
        public int value {protected set; get;}
        public BlockEnum type;

        public Block(StationaryShape shape, IBaseImage image, int PointVal, BlockEnum type) : base(shape, image){
            value = PointVal;
            this.type = type;
        }
        public void BlockHit() {
            health--;

            if (health <= 0) { 
                //break unbreakable block
                //add point to scoreboard with register event "block_destroyed"
                DeleteEntity();
            }
        }

        public virtual void Render() {
            RenderEntity();
        }
    }
}
