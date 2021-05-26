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
    public class HardenedBlock : Block
    {
        public HardenedBlock(StationaryShape shape, IBaseImage image) : base(shape, image) { 
            this.hitPoints = 2; 
        }

        public override void TakeDamage()
        {
            hitPoints -= 1;
            if (hitPoints == 1) {
            // make damaged block
            }
            if (hitPoints <= 0) {
                this.DeleteEntity();  
            }      
        }
    }
    
}