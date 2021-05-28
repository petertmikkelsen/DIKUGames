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

namespace Breakout
{
    public class CollisionControler
    {

        public EntityContainer<Block> blocks;
        public Ball ball;
        public Player player;

        public CollisionControler(EntityContainer<Block> blocks, Ball ball, Player player) {
            this.blocks = blocks;
            this.ball = ball;
            this.player = player; 

        }
        public void CollisionDetector() {

            blocks.Iterate(Block => {
                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), Block.Shape).CollisionDir == CollisionDirection.CollisionDirDown ||
                        CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), Block.Shape).CollisionDir == CollisionDirection.CollisionDirUp) {
                    Block.TakeDamage();
                    ball.SetPositionBlockHitUnderOrAbove();
                }

                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), Block.Shape).CollisionDir == CollisionDirection.CollisionDirRight ||
                        CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), Block.Shape).CollisionDir == CollisionDirection.CollisionDirLeft) {
                    Block.TakeDamage();
                    ball.SetPositionBlockHitSide();

                    
                }
                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.shape).Collision) {
                    float playerPosMid = player.shape.Position.X+player.shape.Extent.X/2.0f;
                    float x = (ball.shape.Position.X-playerPosMid)*0.07f;
                    ball.SetPositionPlayerHit(x);
                }
            });
                
        } 
        
    }
}