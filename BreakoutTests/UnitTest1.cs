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

namespace BreakoutTests {
    [TestFixture]
    public class PlayerTests {
        [SetUp]
        public void SetUp() {
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.0225f)), new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        [Test]
        public void SpawnsWithinBounderies() {
            Assert.True(Player , 1);
        }
    }
}