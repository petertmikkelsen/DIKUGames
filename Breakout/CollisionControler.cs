using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using Breakout.LevelHandling;

namespace Breakout
{
    public class CollisionControler
    {

        public Level level;
        public Ball ball;
        public PlayerBar player;

        public CollisionControler(Ball ball, PlayerBar player, Level level) {
            this.ball = ball;
            this.player = player;
            this.level = level;
        }
        public void CollisionDetector() {
            double closest = 9999;
            Block closestBlock = null;
            CollisionDirection chosenDirection = CollisionDirection.CollisionDirUnchecked;
            level.blocks.Iterate(Block => {
                //Collosion if ball hits above or below
                var info = CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), Block.Shape);
                if (info.Collision && (chosenDirection == CollisionDirection.CollisionDirUnchecked || info.DirectionFactor.Length() < closest)) {
                    closest = info.DirectionFactor.Length();
                    closestBlock = Block;
                    chosenDirection = info.CollisionDir;
                }});
            //
            if (chosenDirection != CollisionDirection.CollisionDirUnchecked) {
                closestBlock.TakeDamage();
                if (chosenDirection == CollisionDirection.CollisionDirDown || chosenDirection == CollisionDirection.CollisionDirUp) {
                    ball.SetPositionBlockHitUnderOrAbove();
                }
                else {
                    ball.SetPositionBlockHitSide();
                }
            }
            //Collision if the ball hits the player
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.shape).Collision) {
                float playerPosMid = player.shape.Position.X+player.shape.Extent.X/2.0f;
                float x = (ball.shape.Position.X-playerPosMid)*0.07f;
                    
                ball.SetPositionPlayerHit(x);
            }
            level.powerUps.Iterate(PowerUp => {
                if (CollisionDetection.Aabb(PowerUp.Shape.AsDynamicShape(), player.shape).Collision) {
                    PowerUp.DeleteEntity();
                    PowerUp.ActivatePowerUp();
                }
            });   
        } 
    }
}