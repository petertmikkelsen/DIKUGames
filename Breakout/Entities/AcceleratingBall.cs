using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;

namespace Breakout.Entities {
    public class AcceleratingBall : IBall {
        public IBall next;
        public float accelerationT0;
        public float accelerationT1;
        public float speed0;
        public float speed1;

        public AcceleratingBall(IBall next, long duration) {
            this.next = next;
            accelerationT0 = StaticTimer.GetElapsedMilliseconds();
            accelerationT1 = accelerationT0 + duration;
            speed0 = 1;
            speed1 = 2;
        }
        public float Speed() {
            long t = StaticTimer.GetElapsedMilliseconds();
            float x = speed0 + (speed1 - speed0) * Math.Clamp((t - accelerationT0) / (accelerationT1 - accelerationT0), 0, 1);
            return next.Speed() * x;
        }
        public void Move(float x) {
            next.Move(x);
        }
        public void SetPositionBlockHitUnderOrAbove() {
            next.SetPositionBlockHitUnderOrAbove();
        }
        public void SetPositionBlockHitSide() {
            next.SetPositionBlockHitSide();
        }
        public void SetPositionPlayerHit(float x, float currentSpeed) {
            next.SetPositionPlayerHit(x, currentSpeed);
        }
        public DynamicShape GetShape(){
            return next.GetShape();
        }
        public void Update() {
            next.Update();
        }
        public void Destroy() {
            next.Destroy();
        }
        public void Render() {
            next.Render();
        }

    }
}