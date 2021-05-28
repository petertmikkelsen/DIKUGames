using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;

namespace Breakout {
    public class Player : Entity{
        public Entity entity;
        public DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private const float movementSpeed = 0.01f;
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image){
            entity = new Entity(shape, image);
            this.shape = shape;
        }
        public void Render() {
            entity.RenderEntity();
        }
        public void Move() {
            if (shape.Position.X >= 0.82f){
                SetMoveRight(false);
                shape.Position.X = 0.82f;
                shape.Move();
            }
            else if (shape.Position.X < 0.01f){
                SetMoveLeft(false);
                shape.Position.X = 0.01f;
                shape.Move();
            }
            else
                shape.Move();
        }
        public void SetMoveLeft(bool val) {
            switch (val)
            {
                case true:
                    moveLeft = -movementSpeed;
                        break;
                case false:
                    moveLeft = 0.0f;
                        break;
            }
            UpdateDirection(); 
        }
        public void SetMoveRight(bool val) {
            switch (val)
            {
                case true:
                    moveRight = movementSpeed;
                        break;
                case false:
                    moveRight = 0.0f;
                        break;
            }
            UpdateDirection();
        }
        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;
        }
    }
}