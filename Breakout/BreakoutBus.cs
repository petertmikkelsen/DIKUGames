using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using Breakout.Utilities;

namespace Breakout {
    public static class BreakoutBus{ 
        private static GameEventBus eventBus;
        public static GameEventBus GetBus() {
            return BreakoutBus.eventBus ?? (BreakoutBus.eventBus =
                new GameEventBus());
        }
    }
}