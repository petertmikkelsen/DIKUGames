using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using Breakout.Utilities;


namespace Breakout {
    public class HardenedBlock : Block {
        public HardenedBlock(StationaryShape shape, IBaseImage image) : base(shape, image, 2, Blocks.BlockEnum.HardenedBlock) { 
            hitPoints = 2; 
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (hitPoints == 1) {
                Image = new Image(ImageDatabase.GetImageFilePath("Darkgreen-block-damaged.png"));
            } 
        }
    }
    
}