using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using NUnit.Framework;
using Breakout;
using DIKUArcade.Math;
using DIKUArcade.Utilities;
using Breakout.Utilities;

namespace BreakoutTests
{
    public class AssertionHelp
    {
        public static void AreEqual(Vec2F vec1, Vec2F vec2, float delta = 0, string message = "vectors not equal") {
            var diff = vec2-vec1;
            if (System.MathF.Abs(diff.X) > delta || System.MathF.Abs(diff.Y) > delta) {
                throw new AssertionException("vectors not equal");
            }
        }
    }
}