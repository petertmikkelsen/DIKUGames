using System;
using System.Collections.Generic;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using System.IO;
using Breakout.Utilities;

namespace Breakout {
    public class PlayerLives : IGameEventProcessor{
        private int numbOfLives;
        private int maxNumbOfLives = 3;
        private EntityContainer lives;
        private IBaseImage heartFilled;
        private IBaseImage heartEmpty;
        public PlayerLives() {
            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
            numbOfLives = maxNumbOfLives;
            lives = new EntityContainer(maxNumbOfLives);
            heartFilled = new Image(ImageDatabase.GetImageFilePath("heart_filled.png"));
            heartEmpty = new Image(ImageDatabase.GetImageFilePath("heart_empty.png"));
            for (int i = 1; i <= maxNumbOfLives; i++) {
                lives.AddStationaryEntity(new StationaryShape(new Vec2F(0.22f - 0.04f * i, 0.94f), new Vec2F(0.04f, 0.04f)), heartFilled);
            }
        }
        public void update() {
            var n = 0;
            foreach(Entity life in lives) {
                if (n < numbOfLives) {
                    life.Image = new Image(ImageDatabase.GetImageFilePath("heart_filled.png"));
                }
                else {
                    life.Image = new Image(ImageDatabase.GetImageFilePath("heart_empty.png"));
                }
                n++;
            }
        }

        public void render() {
            lives.RenderEntities();
        }
        public void GiveLife() {
            if (numbOfLives < maxNumbOfLives)
                numbOfLives++;
        }
        public void TakeLife() {
            numbOfLives--;
            if (numbOfLives <= 0)
                LoseGame();
        }
        public void LoseGame() {
            StateMachine.GetStateMachine().QueueEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent, Message = "GAME_OVER"});
        }
        public void ResetLives() {
            numbOfLives = maxNumbOfLives;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "TAKE_LIFE")
                TakeLife();
            else if (gameEvent.Message == "GIVE_LIFE")
                GiveLife();
            else if (gameEvent.Message == "START_GAME")
                ResetLives();
        }
    }
}