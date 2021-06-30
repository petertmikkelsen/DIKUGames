using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Breakout {
    /// <summary>
    /// 
    /// </summary>
    public static class Constants {

        public static Vec2F BackGroundExtent => new Vec2F(1.0f, 1.0f);
        public static Vec2F BackGroundPosition => new Vec2F(0.0f, 0.0f);
        public static Shape BackGroundShape => new StationaryShape(BackGroundPosition, BackGroundExtent);

    }
}