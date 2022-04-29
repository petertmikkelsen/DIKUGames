namespace Breakout.BreakoutStates {
    public enum LevelEnum { //Enum for the sake of easy switching between the levels. Promotes the singleton pattern & strategy pattern.
        Level_1,
        Level_2,
        Level_3,
        Central_Mass,
        Columns,
        Wall
    }
    public static class LevelTransformer {
        public static string LevelToStr (LevelEnum level) {
            string levelStr = "";
            switch(level) {
                case LevelEnum.Level_1:
                    levelStr = @"C:\Users\Peter\Desktop\DIKUGames-master\Breakout\Assets\Levels\level1.txt";
                    break;
                case LevelEnum.Level_2:
                    levelStr = @"Assests/Levels/level2.txt";
                    break;
                case LevelEnum.Level_3:
                    levelStr = @"Assests/Levels/level3.txt";
                    break;
                case LevelEnum.Central_Mass:
                    levelStr = @"Assests/Levels/central-mass.txt";
                    break;
                case LevelEnum.Columns:
                    levelStr = @"Assests/Levels/columns.txt";
                    break;
                case LevelEnum.Wall:
                    levelStr = @"Assests/Levels/wall.txt";
                    break;
                default:
                    break;
            }
            return levelStr;
        }
    }
}