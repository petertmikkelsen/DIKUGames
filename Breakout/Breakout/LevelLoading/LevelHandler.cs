/*using System;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using System.Collections.Generic;
using DIKUArcade.Events;

namespace Breakout.LevelLoading {
    public class LevelHandler {
        private LevelEnum currLevel;

        public LevelHandler() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        }

        private void StartGame() {
            currLevel = LevelEnum.Level_1;
            StartNewLevel(LevelTransformer.LevelToStr(currLevel));
        }

        public void SwitchLevel () {
            int nextLevel = (int)currLevel + 1;
            if (Enum.IsDefined(typeof(LevelEnum), nextLevel)) {
                currLevel = (LevelEnum)nextLevel;
                StartNewLevel(LevelTransformer.LevelToStr(currLevel));
            }
            else {
                //switch state to gamecomplete
            }
        }

        public void StartNewLevel(string nextLvl) {
            BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, StringArg1 = nextLvl, Message = "NEXT_LEVEL"});
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "START_GAME") {
                StartGame();
            }
        }
    }
}*/