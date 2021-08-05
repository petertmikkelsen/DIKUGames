using System.IO;
using System.Collections.Generic;
using DIKUArcade.Events;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout.Utilities;

namespace Breakout {
    public class ExplosionContainer : AnimationContainer, IGameEventProcessor {
        private List<Image> explosionStrides;
        private const int EXPLOSION_LENGTH_MS = 500;

        public ExplosionContainer() : base(50) {
            BusBuffer.GetBuffer().Subscribe(GameEventType.MovementEvent, this);
            explosionStrides = ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion-HD.png"));
        }
        public void AddExplosion(Vec2F position) {
            AddAnimation(new DynamicShape(position, Constants.ExplosionExtent), EXPLOSION_LENGTH_MS, new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
        }
        public void ProcessEvent(GameEvent gameEvent) {
            if (gameEvent.Message == "EXPLOSION") {
                if (gameEvent.ObjectArg1 != null) {
                    var position = gameEvent.ObjectArg1 as Vec2F;
                    if (position != null)
                        AddExplosion(position);
                }
            }
        }
    }
}