using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;

namespace Breakout
{
    public abstract class Block : Entity {
        public Entity entity;
        public int hitPoints;
        
        public Block(StationaryShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
        }
        public abstract void TakeDamage ();
    }
}