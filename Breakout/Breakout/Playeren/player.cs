using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Events;

namespace Breakout{
    public class Player : Entity, IGameEventProcessor {
        private float moveLeft = 0.0f;
        private float moveRight = 0.0f;
        private const float MOVEMENT_SPEED = 0.01f;
        private Entity entity;
        private DynamicShape shape;
        public Player(DynamicShape shape, IBaseImage image) : base(shape, image) {
            entity = new Entity(shape, image);
            this.shape = shape;
            BreakoutBus.GetBus().Subscribe(DIKUArcade.Events.GameEventType.InputEvent, this);
            BreakoutBus.GetBus().Subscribe(DIKUArcade.Events.GameEventType.GameStateEvent, this);
        }

        public Vec2F GetPosition() {
            return new Vec2F(shape.Position.X + (shape.Extent.X/2.0f), shape.Position.Y + 0.01f);
        }

        public void KeyPress(string key) {
            switch (key) {
                case "KEY_LEFT":
                    SetMoveLeft(true);
                    break;
                case "KEY_RIGHT":
                    SetMoveRight(true);
                    break;
                default:
                    break;
            }
        }

        public void KeyRelease(string key) {
            switch (key) {
                case "KEY_LEFT":
                    SetMoveLeft(false);
                    break;
                case "KEY_RIGHT":
                    SetMoveRight(false);
                    break;
                default:
                    break;
            }
        }
        public void Move() {
            var moveToX = shape.Position.X + moveLeft + moveRight;
            if (moveToX > 0 && moveToX + shape.Extent.X < 1.0f)
                shape.Move();
        }
        private void SetMoveLeft(bool val) {
            if (val) 
                moveLeft = -MOVEMENT_SPEED;
            else moveLeft = 0;
            UpdateDirection();
        }
        private void SetMoveRight(bool val) {
            if (val) 
                moveLeft = +MOVEMENT_SPEED;
            else moveLeft = 0;
            UpdateDirection();
        }

        public void Render() {
            entity.RenderEntity();
        }
        private void UpdateDirection() {
            //shape.AsDynamicShape().Direction = new Vec2F((moveLeft + moveRight), 0.0f);
            shape.Direction.X = moveLeft + moveRight;
        }
        
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "KEY_PRESS"){
                KeyPress(gameEvent.StringArg1);
                System.Console.WriteLine("2");
            }
            else if (gameEvent.Message == "KEY_RELEASE")
                KeyRelease(gameEvent.StringArg1);
        }
    }    
}