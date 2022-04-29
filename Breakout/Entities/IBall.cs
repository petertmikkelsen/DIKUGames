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
    public interface IBall {
        void Update();
        void Destroy();
        void Render();
        float Speed();
        void Move(float speed);
        void SetPositionPlayerHit(float x, float currentSpeed);
        void SetPositionBlockHitSide();
        void SetPositionBlockHitUnderOrAbove();
        DynamicShape GetShape();
    }
}