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
            DIKUArcade.GUI.Window.CreateOpenGLContext();
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.17f, 0.0225f)), null);
            levelLoader = new LevelLoader();
            levelLoader.GetSubfiles(Path.Combine(FileIO.GetProjectPath(), "Assets", "Levels", "level1.txt"));

        }

        [Test]
        // Tjekker at spilleren ikke kan bevæge sig ud over den venstre kant
        public void MovesWithinBounderiesLeft() {
            for (int i = 0; i < 100; i++) {
                player.SetMoveLeft(true);
                player.Move();
            }
            Assert.AreEqual(0.01f, player.shape.Position.X);
        }

        [Test]
        // Tjekker at spilleren ikke kan bevæge sig ud over den højre kant
        public void MovesWithinBounderiesRight() {
            for (int i = 0; i < 100; i++) {
                player.SetMoveRight(true);
                player.Move();
            }
            Assert.AreEqual(0.82f, player.shape.Position.X);
        }

        [Test]
        // Tjekker at spilleren stater i midten i den nederste den af vinduet 
        public void SpawnsInMiddle() {
            Assert.AreEqual(0.41f, player.shape.Position.X);
            Assert.AreEqual(0.1f, player.shape.Position.Y);
        }

        [Test]
        // Map er altid 25 linjer langt
        public void MapTest() {
            Assert.AreEqual(25 , levelLoader.map.Length);
        }

        [Test]
        // I denne test tjekker vi om string arrayet indeholder et ":" da meta-delen
        // af .txt filen er den eneste der indeholder sådan et tegn på hver linje.
        public void MetaTest() {
            foreach (string line in levelLoader.meta) {
                if (!line.Contains(":")) {
                    retValMeta = false;
                }
                Assert.AreEqual(true, retValMeta);
            }
        }

        [Test]
        // Legend er den eneste del af tekstfilen som indeholder ")" på hver linje.
        public void LegendTest() {
            foreach (string line in levelLoader.legend) {
                if (!line.Contains(")")) {
                    retValLegend = false;
                }
                Assert.AreEqual(true, retValLegend);
            }
        }
    }
}