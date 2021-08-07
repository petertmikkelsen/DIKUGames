using System;
using DIKUArcade.GUI;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Breakout {
    class Program {
        static void Main(string[] args) {
            
            var windowArgs = new WindowArgs();
            windowArgs.Title = "Breakout";
            windowArgs.Height = 900U;
            windowArgs.Width = 900U;

            
            var game = new Game(windowArgs);
            game.Run();
        }
    }
}