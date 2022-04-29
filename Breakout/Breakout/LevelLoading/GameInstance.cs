using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using Breakout.Blocks;
using Breakout.BreakoutStates;

namespace Breakout.LevelLoading {
    public class GameInstance {
        private LevelInstance level;
        public LevelEnum levelEnum;

        public GameInstance() {
            GoToLevel(LevelEnum.Level_1);
        }

        public void GoToLevel(LevelEnum E) {
            levelEnum = E;
            level = new LevelInstance(new LevelReading().LoadDefinition(levelEnum));
        }
        public void render() {
            level.Render();
        }
        public void Update() {
            level.Update();
        }
    }
}