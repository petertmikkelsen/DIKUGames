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
    public class PlayerBar : Entity, IGameEventProcessor
    {
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private const float movementSpeed = 0.01f;
        private int invisCounter;
        public PlayerBar(DynamicShape xShape, IBaseImage image) : base(xShape, image) {
            Shape = Shape.AsDynamicShape();
            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
        }
        public void Destroy() {
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.GameStateEvent, this);
        }
        public void Render()
        {
            RenderEntity();
        }
        public void Move()
        {
            if (Shape.Position.X >= 0.82f)
            {
                SetMoveRight(false);
                Shape.Position.X = 0.82f;
                Shape.Move();
            }
            else if (Shape.Position.X < 0.01f)
            {
                SetMoveLeft(false);
                Shape.Position.X = 0.01f;
                Shape.Move();
            }
            else
                Shape.Move();
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
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent, StringArg1 = "KEY_RIGHT", Message = "KEY_RELEASE"});
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                            EventType = GameEventType.GameStateEvent, StringArg1 = "KEY_LEFT", Message = "KEY_RELEASE"});
                        BreakoutBus.GetBus().RegisterEvent(new GameEvent {
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
            GetShape().Direction.X = moveLeft + moveRight;
        }
        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }
        public void ResetPosition()
        {
            Shape.Position = Constants.PlayerStartPosition;
        }

        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "INVISIBLE_PLAYER") {
                if (invisCounter != 0) {
                    isVisible = false;
                }
                invisCounter++;
            }
            if (gameEvent.Message == "VISIBLE_PLAYER") {
                isVisible = true;
                invisCounter--;
            }
        }
    }
}