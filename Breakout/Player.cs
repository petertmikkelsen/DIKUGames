using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.IO;
using DIKUArcade.Graphics;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Entities;
using DIKUArcade.Input;

namespace Breakout
{
    public class Player : Entity, IGameEventProcessor
    {
        public Entity entity;
        public DynamicShape shape;
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private const float movementSpeed = 0.01f;
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image)
        {
            entity = new Entity(shape, image);
            this.shape = shape;
            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
        }
        public void Destroy() {
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.GameStateEvent, this);
        }
        public void Render()
        {
            entity.RenderEntity();
        }
        public void Move()
        {
            if (shape.Position.X >= 0.82f)
            {
                SetMoveRight(false);
                shape.Position.X = 0.82f;
                shape.Move();
            }
            else if (shape.Position.X < 0.01f)
            {
                SetMoveLeft(false);
                shape.Position.X = 0.01f;
                shape.Move();
            }
            else
                shape.Move();
        }
        public void SetMoveLeft(bool val)
        {
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
        public void SetMoveRight(bool val)
        {
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

        public bool GameKeyEvent(KeyboardAction action, KeyboardKey key) {
            if (action == KeyboardAction.KeyPress) {
                switch (key) {
                    case KeyboardKey.Left:
                        SetMoveLeft(true);
                        return true;
                    case KeyboardKey.Right:
                        SetMoveRight(true);
                        return true;
                    case KeyboardKey.J:
                        SetMoveLeft(true);
                        return true;
                    case KeyboardKey.L:
                        SetMoveRight(true);
                        return true;
                    case KeyboardKey.P:
                        StateMachine.GetStateMachine().QueueEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent, StringArg1 = "KEY_RIGHT", Message = "KEY_RELEASE"});
                        StateMachine.GetStateMachine().QueueEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent, StringArg1 = "KEY_LEFT", Message = "KEY_RELEASE"});
                        StateMachine.GetStateMachine().QueueEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent, Message = "GAME_PAUSED"});
                        StateMachine.GetStateMachine().SwitchState(GameStateType.GamePaused);
                        return true;
                }
            }
            else if (action == KeyboardAction.KeyRelease)
            {
                switch (key)
                {
                    case KeyboardKey.Left:
                        SetMoveLeft(false);
                        return true;
                    case KeyboardKey.Right:
                        SetMoveRight(false);
                        return true;
                    case KeyboardKey.J:
                        SetMoveLeft(false);
                        return true;
                    case KeyboardKey.L:
                        SetMoveRight(false);
                        return true;
                    default:
                        return true;
                }
            }
            return false;
        }

        private void UpdateDirection()
        {
            shape.Direction.X = moveLeft + moveRight;
        }

        public void ResetPosition()
        {
            shape.Position = Constants.PlayerStartPosition;
        }

        public void ProcessEvent(GameEvent gameEvent) {
        }
    }
}