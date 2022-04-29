using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using Breakout.Blocks;

namespace Breakout {
    public class DefaultBlock : Block {
        public DefaultBlock(StationaryShape shape, IBaseImage image) : base(shape, image, 1, Blocks.BlockEnum.Default) {
            health = 1;
        }
    }
}