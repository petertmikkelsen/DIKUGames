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
        public Ball ball {private set; get;}
        public Player player {private set; get;}
        private Text showPoints;
        private int points = 0;
        private PlayerLives playerLives;
        private bool frozen;
        private Stopwatch freezeTimer;

        private Level level;
        public LevelEnum levelEnum;

        public CollisionControler collisionControler {private set; get;}
        private ExplosionContainer explosionContainer;
        

        public GameInstance() {
            player = new Player(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.0225f)), new Image(ImageDatabase.GetImageFilePath("Player.png")));
            ball = new Ball(new DynamicShape(new Vec2F(0.485f, 0.1225f), new Vec2F(0.03f, 0.03f), new Vec2F(0.006f, 0.009f)*1.5f), 
                new Image(ImageDatabase.GetImageFilePath("ball.png")));
            showPoints = new Text(points.ToString(), new Vec2F(0.90f, 0.70f), new Vec2F(0.3f, 0.3f));
            showPoints.SetColor(System.Drawing.Color.BlanchedAlmond); 
            showPoints.SetFontSize(60);
            playerLives = new PlayerLives();
            freezeTimer = new Stopwatch();

            collisionControler = new CollisionControler (ball, player);
            explosionContainer = new ExplosionContainer();
            
            GoToLevel(LevelEnum.Level_1);

            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
        }
        public bool GameKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (freezeTimer.ElapsedMilliseconds > 50) {
                if (player.GameKeyEvent(action, key)) {
                    SetFrozen(false);
                    return true;
                }
            }
            return false;
        }
        public void Destroy() {
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.GameStateEvent, this);
            player.Destroy();
            level.Destroy();
        }
        public void GoToLevel(LevelEnum E) {
            if (level != null) 
                level.Destroy();
            levelEnum = E;
            level = new Level(new LevelLoader().LoadDefinition(levelEnum));
            collisionControler.level = level;
            ball.ResetPosition();
            player.ResetPosition();
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
            ball.RenderEntity();
            player.RenderEntity();
            showPoints.RenderText();
            level.render();
            explosionContainer.RenderAnimations();
            playerLives.render();
        }
        public void update() {
            if (frozen == false) {
                player.Move();
                ball.Move();
                collisionControler.CollisionDetector();
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
                player.ResetPosition();
                ball.ResetPosition();
                SetFrozen(true);
            }
            if (gameEvent.Message == "STATE_CHANGE") {
                if ()
            }
                
        }
    }
}