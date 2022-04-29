using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class Score {
        public int score {get; private set;}
        public Text display {get; private set;}
        public Vec2F position;
        public Vec2F extent;
        public Score (Vec2F position, Vec2F extent) {
            this.position = position;
            this.extent = extent;
            score = 0;
            display = new Text (score.ToString(), position, extent);
        }
        public void AddPoints () {
            score++;
        }
        public void RenderScore () {
            display.SetColor(System.Drawing.Color.White);
            display.RenderText();
        }
        public void Update() {
            display.SetText("Score: " + score.ToString());
        }
    }
}