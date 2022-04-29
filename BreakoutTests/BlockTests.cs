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
        private LevelLoader levelLoader0;
        public EntityContainer<Block> blocks;
        [SetUp]
        public void Setup() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            levelLoader0 = new LevelLoader();
            levelLoader0.GetSubfiles(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "testmap.txt"));
            levelCreator = new LevelCreator(blocks);
            levelCreator.LoadNewlevel(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "testmap.txt"));
        }

        [Test]
        public void blocksSpawnsCorrectly() {
            foreach (Block block in blocks)
                Assert.AreEqual(block.Shape.Position, (0,1f, 0,92f));
        }
    }
}