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
    public class LevelTesting {
        private LevelLoader levelLoader0;
        private LevelLoader levelLoader1;
        private LevelLoader levelLoader2;
        private LevelCreator levelCreator;
        public EntityContainer<Block> blocks;
        bool retValMeta = true;
        bool retValLegend = true;

        [SetUp]
        public void SetUp() {
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            levelLoader0 = new LevelLoader();
            levelLoader0.GetSubfiles(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "level1.txt"));
            levelLoader1 = new LevelLoader();
            levelLoader1.GetSubfiles(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "level2.txt"));
            levelLoader2 = new LevelLoader();
            levelLoader2.GetSubfiles(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "level3.txt"));
        }

        [Test]
        // Map er altid 25 linjer langt
        public void MapTest0() {
            Assert.AreEqual(25 , levelLoader0.map.Length);
        }

        [Test]
        public void MapTest1() {
            Assert.AreEqual(25 , levelLoader1.map.Length);
        }

        [Test]
        public void MapTest2() {
            Assert.AreEqual(25 , levelLoader2.map.Length);
        }

        [Test]
        // I denne test tjekker vi om string arrayet indeholder et ":" da meta-delen
        // af .txt filen er den eneste der indeholder sådan et tegn på hver linje.
        public void MetaTest0() {
            foreach (string line in levelLoader0.meta) {
                if (!line.Contains(":")) {
                    retValMeta = false;
                }
                Assert.AreEqual(true, retValMeta);
            }
        }

        [Test]
        public void MetaTest1() {
            foreach (string line in levelLoader1.meta) {
                if (!line.Contains(":")) {
                    retValMeta = false;
                }
                Assert.AreEqual(true, retValMeta);
            }
        }

        [Test]
        public void MetaTest2() {
            foreach (string line in levelLoader2.meta) {
                if (!line.Contains(":")) {
                    retValMeta = false;
                }
                Assert.AreEqual(true, retValMeta);
            }
        }

        [Test]
        // Legend er den eneste del af tekstfilen som indeholder ")" på hver linje.
        public void LegendTest0() {
            foreach (string line in levelLoader0.legend) {
                if (!line.Contains(")")) {
                    retValLegend = false;
                }
                Assert.AreEqual(true, retValLegend);
            }
        }

        [Test]
        // Legend er den eneste del af tekstfilen som indeholder ")" på hver linje.
        public void LegendTest1() {
            foreach (string line in levelLoader1.legend) {
                if (!line.Contains(")")) {
                    retValLegend = false;
                }
                Assert.AreEqual(true, retValLegend);
            }
        }

        [Test]
        public void LegendTest2() {
            foreach (string line in levelLoader2.legend) {
                if (!line.Contains(")")) {
                    retValLegend = false;
                }
                Assert.AreEqual(true, retValLegend);
            }
        }

        [Test]
        public void InvalidFileName() {
            Assert.Throws<FileNotFoundException>(WrongInputException);
        }
        void WrongInputException() {
            levelCreator = new LevelCreator(blocks);
            levelCreator.LoadNewlevel(ImageDatabase.GetImageFilePath("Anders And"));
        }
    }
}