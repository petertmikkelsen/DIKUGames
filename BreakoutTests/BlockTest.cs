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
    public class BlockTest
    {
        private HardenedBlock testHardenedBlock;
        private NormalBlock testNormalBlock;
        [SetUp]
        public void setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            testHardenedBlock = new HardenedBlock(
                    new StationaryShape(new Vec2F(0.08f, 0.03f), new Vec2F(0.08f, 0.03f)),
                    new Image(Path.Combine("Assets", "Images", "darkgreen-block.png")));
            testNormalBlock = new NormalBlock(
                    new StationaryShape(new Vec2F(0.08f, 0.03f), new Vec2F(0.08f, 0.03f)),
                    new Image(Path.Combine("Assets", "Images", "teal-block.png")));
        }
        [Test]
        public void HardenedBlockTest() {
            testHardenedBlock.TakeDamage();
            testHardenedBlock.TakeDamage();
            Assert.AreEqual(testHardenedBlock.IsDeleted(), true);
        }

        [Test]
        public void NormalBlockTest() {
            testNormalBlock.TakeDamage();
            Assert.AreEqual(testNormalBlock.IsDeleted(), true);
        }

        [Test]
        public void ImageChangeTest() {
            testHardenedBlock.TakeDamage();
            var result = testHardenedBlock.Image;
            Assert.That(result, Is.EqualTo(new Image(Path.Combine("Assets", "Images", "darkgreen-block-damaged.png"))));
        }
    }
}