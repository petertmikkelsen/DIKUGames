using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using Breakout.Entities;
using DIKUArcade.Math;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using Breakout.LevelHandling;

namespace Breakout
{
    public class CollisionControler
    {

        public Level level;
        public PlayerBar player;

        public CollisionControler(PlayerBar player, Level level) {
            this.player = player;
            this.level = level;
        }
        public void CollisionDetector() {
            double closest = 9999;
            Block closestBlock = null;
            CollisionDirection chosenDirection = CollisionDirection.CollisionDirUnchecked;
            level.blocks.Iterate(Block => {
                //Collosion if ball hits above or below
                var info = CollisionDetection.Aabb(level.ball.GetShape().AsDynamicShape(), Block.Shape);
                if (info.Collision && (chosenDirection == CollisionDirection.CollisionDirUnchecked || info.DirectionFactor.Length() < closest)) {
                    closest = info.DirectionFactor.Length();
                    closestBlock = Block;
                    chosenDirection = info.CollisionDir;
                }});
            //
            if (chosenDirection != CollisionDirection.CollisionDirUnchecked) {
                closestBlock.TakeDamage();
                if (chosenDirection == CollisionDirection.CollisionDirDown || chosenDirection == CollisionDirection.CollisionDirUp) {
                    level.ball.SetPositionBlockHitUnderOrAbove();
                }
                else {
                    level.ball.SetPositionBlockHitSide();
                }
            }
            //Collision if the ball hits the player
            if (CollisionDetection.Aabb(level.ball.GetShape().AsDynamicShape(), player.Shape).Collision) {
                float playerPosMid = player.Shape.Position.X+player.Shape.Extent.X/2.0f;
                float x = (level.ball.GetShape().Position.X-playerPosMid)*0.07f;
                    
                level.ball.SetPositionPlayerHit(x,level.ball.Speed());
            }
            level.powerUps.Iterate(PowerUp => {
                if (CollisionDetection.Aabb(PowerUp.Shape.AsDynamicShape(), player.Shape).Collision) {
                    PowerUp.DeleteEntity();
                    PowerUp.ActivatePowerUp();
                }
            });   
        } 
    }
}