using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;

namespace Breakout
{
    public abstract class Block : Entity {
        public int hitPoints {protected set; get;}
        public int PointValue {protected set; get;}
        
        public Block(StationaryShape shape, IBaseImage image, int PointVal) : base(shape, image){
            PointValue = PointVal;
        }
        public virtual void TakeDamage() {
            hitPoints -= 1;
            if (hitPoints <= 0) {
                this.DeleteEntity();
                var game = StateMachine.GetStateMachine().GetGameState(GameStateType.GameRunning) as GameRunning;
                game.AddPoints(PointValue);
            } 
        }
    }
}