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
        private const float E = (float) 1e-6;
        private LevelCreator levelCreator;
        // private LevelLoader levelLoader0;
        public EntityContainer<Block> blocks;
        [SetUp]
        public void Setup() {
            System.IO.Directory.SetCurrentDirectory("C:/Users/Peter/Documents/C#/EksamenProjekt/DIKUGames/Breakout");
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            // levelLoader0 = new LevelLoader();
            // levelLoader0.GetSubfiles(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "testmap.txt"));
            blocks = new EntityContainer<Block>();
            levelCreator = new LevelCreator(blocks);
            levelCreator.LoadNewlevel(Path.Combine("Assets", "Levels", "testmap.txt"));
        }

        [Test]
        public void blocksSpawnsCorrectly() {
            foreach (Block block in blocks)
                AssertionHelp.AreEqual(block.Shape.Position, new Vec2F(0.1f, 0.92f), E);
        }
    }
}