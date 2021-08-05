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

namespace Breakout
{
    public abstract class Block : Entity {
        public int hitPoints {protected set; get;}
        public int pointValue {protected set; get;}
        public BlockEnum type;
        
        public Block(StationaryShape shape, IBaseImage image, int PointVal, BlockEnum type) : base(shape, image){
            pointValue = PointVal;
            this.type = type;
        }
        public virtual void TakeDamage() {
            hitPoints -= 1;
            if (hitPoints <= 0) {
                this.DeleteEntity();
                var game = StateMachine.GetStateMachine().GetGameState(GameStateType.GameRunning) as GameRunning;
                StateMachine.GetStateMachine().QueueEvent(new GameEvent{
                    EventType = GameEventType.GameStateEvent, IntArg1 = pointValue, Message = "ADD_POINTS"});
                StateMachine.GetStateMachine().QueueEvent(new GameEvent{
                   EventType = GameEventType.GameStateEvent, Message = "BLOCK_DESTROYED"});
            } 
        }
    }
}