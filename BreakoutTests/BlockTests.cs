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
    public class BlockTests
    {
        private LevelCreator levelCreator;
        private EntityContainer<Block> blocks;
        [SetUp]
        public void Setup() {
            levelCreator = new LevelCreator(blocks);
            levelCreator.LoadNewlevel(ImageDatabase.GetImageFilePath("Test.txt"));
        }

        [Test]
        public void blocksSpawnsCorrectly() {
            blocks.Iterate(Block => {
                Assert.AreEqual(Block.Shape.Position, (0,1f, 0,92f));
                Assert.AreEqual(Block.Shape.Position, (0,26f, 0,92f));
            });
        }
    }
}