using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;

namespace Breakout {
    public class NormalBlock : Block {
        public NormalBlock(StationaryShape shape, IBaseImage image) : base(shape, image, 1, Blocks.BlockEnum.NormalBlock) {
            hitPoints = 1;
        }
    }
}