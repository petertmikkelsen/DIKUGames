namespace Breakout.LevelHandling
{
    public enum LevelEnum {
        Level_1,
        Level_2,
        Level_3,
        Central_Mass,
        Columns,
        Wall,
        End
    }

    public static class LevelToString {
        public static string LevelEnumToString (LevelEnum level) {
            string levelAsString = "";
            switch (level) {
                case LevelEnum.Level_1:
                    levelAsString = @"Assets/levels/level1.txt";
                    break;
                case LevelEnum.Level_2:
                    levelAsString = @"Assets/levels/level2.txt";
                    break;
                case LevelEnum.Level_3:
                    levelAsString = @"Assets/levels/level3.txt";
                    break;
                case LevelEnum.Central_Mass:
                    levelAsString = @"Assets/levels/central-mass.txt";
                    break;
                case LevelEnum.Columns:
                    levelAsString = @"Assets/levels/columns.txt";
                    break;
                case LevelEnum.Wall:
                    levelAsString = @"Assets/levels/wall.txt";
                    break;
                default:
                    break;
            }
            return levelAsString;
        }
    }
}