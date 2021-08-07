using System;
using System.Drawing;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Math;
using Breakout.Utilities;
using DIKUArcade.Graphics;
using Breakout.Blocks;
using DIKUArcade.Events;
using DIKUArcade.Timers;
using DIKUArcade.Input;

namespace Breakout.LevelHandling {
    public class Level : IGameEventProcessor {
        public EntityContainer<Block> blocks { private set; get; }
        public EntityContainer<PowerUp> powerUps { private set; get; }
        public Ball ball {private set; get;}
        public PlayerBar player {private set; get;}
        public CollisionControler collisionControler {private set; get;}
        private Random random;
        public bool hasStarted = false;
        public long startTime;
        public long levelTime;
        public Text timeLeft;
        public Level(LevelDefinition levelDefinition) {
            blocks = new EntityContainer<Block>();
            powerUps = new EntityContainer<PowerUp>();
            for (int i = 0; i < levelDefinition.map.Length; i ++) {
                for (int j = 0; j < levelDefinition.map[i].Length; j++) {
                    if (levelDefinition.map[i][j] != '-') {
                        string c = Char.ToString(levelDefinition.map[i][j]);
                        CreateBlock(new Vec2F(0.02f + j * 0.08f, 0.95f - 
                            i * 0.03f), levelDefinition.legendDictionary[c], levelDefinition.GetBlockType(c));
                    }                        
                }
            }
            player = new PlayerBar(
                new DynamicShape(new Vec2F(0.41f, 0.1f), new Vec2F(0.18f, 0.0225f)),
                ImageDatabase.GetInstance().GetImage("Player.png"));
            SpawnDefaultBall();
            collisionControler = new CollisionControler (ball, player, this);

            levelTime = 1000 * long.Parse(levelDefinition.metaDictionary["Time"]);
            timeLeft = new Text("Time: "+(levelTime/1000).ToString(), new Vec2F(0.4f, 0.685f), new Vec2F(0.3f, 0.3f));
            timeLeft.SetColor(System.Drawing.Color.Black); 
            timeLeft.SetFontSize(60);

            BusBuffer.GetBuffer().Subscribe(GameEventType.GameStateEvent, this);
            BusBuffer.GetBuffer().Subscribe(GameEventType.MovementEvent, this);

            random = new Random();
        }
        private void CreateBlock (Vec2F position, string fileName, BlockEnum block) {
            var shape = new StationaryShape(position, new Vec2F(0.08f, 0.03f));
            var image = ImageDatabase.GetInstance().GetImage(fileName); 
            if (block == BlockEnum.HardenedBlock) {
                blocks.AddEntity(new HardenedBlock(shape,image));
            }
            else if (block == BlockEnum.UnbreakableBlock ) {
                blocks.AddEntity(new UnbreakableBlock(shape,image));
            }
            else if (block == BlockEnum.ExplosiveBlock) {
                blocks.AddEntity(new ExplosiveBlock(shape,image));
            }
            else if (block == BlockEnum.PowerUpBlock) {
                blocks.AddEntity(new PowerUpBlock(shape,image));
            }
            else if (block == BlockEnum.NormalBlock) {
                blocks.AddEntity(new NormalBlock(shape,image));
            }
        }
        public void SpawnDefaultBall() {
            ball = new Ball(new DynamicShape(new Vec2F(0.485f, 0.1225f), new Vec2F(0.03f, 0.03f), new Vec2F(0.006f, 0.009f)*1.5f), 
                ImageDatabase.GetInstance().GetImage("ball.png"));
            if (collisionControler != null) {
                collisionControler.ball = ball;
            }
        }
        public void Start() {
            if (hasStarted == false) {
                startTime = StaticTimer.GetElapsedMilliseconds();
                hasStarted = true;
            }
        }
        public bool HasExpired() {
            if (hasStarted && StaticTimer.GetElapsedMilliseconds() - startTime > levelTime) {
                return true;
            }
            return false;
        }
        public bool GameKeyEvent(KeyboardAction action, KeyboardKey key) {
            return player.GameKeyEvent(action, key);
        }
        public void Destroy() {
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.GameStateEvent, this);
            BusBuffer.GetBuffer().Unsubscribe(GameEventType.MovementEvent, this);
            player.Destroy();
            ball.Destroy();
        }
        public void render() {
            blocks.RenderEntities();
            timeLeft.RenderText();
            ball.RenderEntity();
            player.RenderEntity();
            powerUps.RenderEntities();
        }
        public void update() {
            player.Move();
            ball.Update();
            collisionControler.CollisionDetector();
            foreach (PowerUp powerUp in powerUps) {
                powerUp.Move();
            }
            timeLeft.SetText("Time: "+((levelTime - (StaticTimer.GetElapsedMilliseconds() - startTime))/1000).ToString());
            if (HasExpired()) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.GameStateEvent, Message = "GAME_OVER"});
            }
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "BLOCK_DESTROYED" && blocks.CountEntities() == 0) {
                BreakoutBus.GetBus().RegisterEvent(new GameEvent{
                    EventType = GameEventType.GameStateEvent, Message = "LEVEL_COMPLETED"});
            }
            if (gameEvent.Message == "SPAWN_POWERUP") {
                var rnd = (PowerUpEnum)random.Next(0,(int)PowerUpEnum.POWER_UP_ENUM_END);
                powerUps.AddEntity(PowerUpController.CreatePowerUp(gameEvent.ObjectArg1 as Vec2F, rnd));
            }
            if (gameEvent.Message == "BALL_DEAD") {
                if (ball != null) {
                    ball.Destroy();
                    ball.DeleteEntity();
                }
                SpawnDefaultBall();
                BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent, Message = "TAKE_LIFE"});
            }
            if (gameEvent.Message == "EXPLOSION") {
                foreach (Block block in blocks) {
                    if (gameEvent.ObjectArg1 != null) {
                        var position = gameEvent.ObjectArg1 as Vec2F;
                        if (position != null) {
                        var range = new Vec2F(0.1f, 0.05f);
                            if (Math.Abs(block.Shape.Position.X - position.X) <= range.X && Math.Abs(block.Shape.Position.Y - position.Y) <= range.Y)
                                block.TakeDamage();
                        }
                    }
                }
            }
        } 
    }
}