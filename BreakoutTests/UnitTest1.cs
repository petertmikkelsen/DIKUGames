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

namespace BreakoutTests {
    public class PlayerTests {
        public Player player;
        private LevelLoader levelLoader;
        //string path = Directory.GetCurrentDirectory();
        //string path2 = Directory.GetParent(path);
        bool retValMeta = true;
        bool retValLegend = true;

        [SetUp]
        public void SetUp() {
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.17f, 0.0225f)), null);
            levelLoader = new LevelLoader();
            levelLoader.LoadNewlevel(Path.Combine("Assets", "Levels", "level1.txt"));
        }

        [Test]
        public void MovesWithinBounderiesLeft() {
            for (int i = 0; i < 100; i++) {
                player.SetMoveLeft(true);
                player.Move();
            }
            Assert.AreEqual(0.01f, player.shape.Position.X);
        }
        [Test]
        public void MovesWithinBounderiesRight() {
            for (int i = 0; i < 100; i++) {
                player.SetMoveRight(true);
                player.Move();
            }
            Assert.AreEqual(0.82f, player.shape.Position.X);
        }
        [Test]
        // siden at vi sætter spiller billedet ind fra nederste venstre hjørne af figuren, skal vi sætte player.shape.Position.X til
        // at være det halve af dens brede
        public void SpawnsInMiddle() {
            Assert.AreEqual(0.41f, player.shape.Position.X);
            Assert.AreEqual(0.1f, player.shape.Position.Y);
        }
        [Test]
        public void MapTest() {
            Assert.AreEqual(25 , levelLoader.map.Length);
        }
        [Test]
        // I denne test tjekker vi om string arrayet indeholder et ":" da meta delen af .txt filen er den eneste der indeholder sådan et tegn på hver linje.
        public void MetaTest() {
            foreach (string line in levelLoader.meta) {
                if (!line.Contains(":")) {
                    retValMeta = false;
                }
                Assert.Equals(true, retValMeta);
            }
            
        }
        [Test]
        public void LegendTest() {
            foreach (string line in levelLoader.legend) {
                if (!line.Contains(")")) {
                    retValLegend = false;
                }
                Assert.Equals(true, retValLegend);
            }
        }
        [Test]
        public void BlockTest() {

        }
    }
}