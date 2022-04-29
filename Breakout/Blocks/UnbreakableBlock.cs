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

namespace Breakout.Blocks {
    public class UnbreakableBlock : Block {
        public UnbreakableBlock(StationaryShape shape, IBaseImage image) : base (shape, image, 1, Blocks.BlockEnum.UnbreakableBlock) {
            hitPoints = 1;
        }
        public override void TakeDamage() {   
        }
    }
}