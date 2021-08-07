using DIKUArcade.Math;
using Breakout.PowerUps;

namespace Breakout {
    public class PowerUpController {
        public static PowerUp CreatePowerUp(Vec2F position, PowerUpEnum type) {
            switch(type) {
                case PowerUpEnum.ExtraLife:
                    return new ExtraLife(position);
                case PowerUpEnum.ExtraPoints:
                    return new ExtraPoints(position);
                case PowerUpEnum.DoubleSize:
                    return new DoubleSize(position);
                case PowerUpEnum.DoubleSpeed:
                    return new DoubleSpeed(position);
                case PowerUpEnum.HalfSpeed:
                    return new HalfSpeed(position);
                case PowerUpEnum.InvisibleBall:
                    return new InvisibleBall(position);
                case PowerUpEnum.InvisiblePlayer:
                    return new InvisiblePlayer(position);
            default:
                return null;
            }
        }
    }
}