using System;
using System.Collections.Generic;
using Breakout;
using Breakout.Utilities;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using System.Diagnostics;

namespace Breakout.LevelHandling
{
    public class GameInstance : IGameEventProcessor{

        private Text showPoints;
        private int points = 0;
        private PlayerLives playerLives;
        private bool frozen;
        private Stopwatch freezeTimer;
        

        private Level level;
        public LevelEnum levelEnum;

        
        private ExplosionContainer explosionContainer;
        

        public GameInstance() {
            
            showPoints = new Text(points.ToString(), new Vec2F(0.90f, 0.685f), new Vec2F(0.3f, 0.3f));
            showPoints.SetColor(System.Drawing.Color.Black); 
            showPoints.SetFontSize(60);

            playerLives = new PlayerLives();
            freezeTimer = new Stopwatch();
            
            explosionContainer = new ExplosionContainer();
            
            GoToLevel(LevelEnum.Level_1);

            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);

            
        }
        public bool GameKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (freezeTimer.ElapsedMilliseconds > 50) {
                if (level.GameKeyEvent(action, key)) {
                    SetFrozen(false);
                    return true;
                }
            }
            return false;
        }
        public void Destroy() {
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.GameStateEvent, this);
            level.Destroy();
        }
        public void GoToLevel(LevelEnum E) {
            if (level != null) 
                level.Destroy();
            levelEnum = E;
            level = new Level(new LevelLoader().LoadDefinition(levelEnum));
            SetFrozen(true);
        }
        public void AddPoints(int p) {
            points += p;
            showPoints.SetText(points.ToString());
            //Console.WriteLine(points);
        }
        public void SetFrozen(bool newState) {
            if (newState) {
                level.Start();
                freezeTimer.Start();
                StaticTimer.PauseTimer();
            }
            else {
                StaticTimer.ResumeTimer();
            }
            frozen = newState;
        }

        public void render() {
            showPoints.RenderText();
            level.render();
            explosionContainer.RenderAnimations();
            playerLives.render();
        }
        public void update() {
            if (frozen == false) {
                
                level.update();
            }
            playerLives.update();
            
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "ADD_POINTS")
                AddPoints(gameEvent.IntArg1);
            if (gameEvent.Message == "LEVEL_COMPLETED") {
                var nextLevel = levelEnum + 1;
                if (nextLevel != LevelEnum.End) {
                    GoToLevel(nextLevel);
                }
                else {}
            }
            if (gameEvent.Message == "TAKE_LIFE") {
                level.player.ResetPosition();
                level.ball.ResetPosition();
                SetFrozen(true);
            }
            if (gameEvent.Message == "STATE_CHANGE") {
                SetFrozen(!StateMachine.GetStateMachine().IsGameState(GameStateType.GameRunning));
            }
            if (gameEvent.Message == "EXTRA_LIFE") {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.GameStateEvent, Message = "GIVE_LIFE"});
            }
            if (gameEvent.Message == "EXTRA_POINTS") {
                AddPoints(10);
            }
        }
    }
}