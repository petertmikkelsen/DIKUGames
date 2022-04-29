namespace Breakout.Blocks {
    public enum BlockEnum {
        Default
    }
    public static class BlockTransformer {
        public static BlockEnum StrToBlock (string blockStr) { //Adapter pattern
            BlockEnum BlockEnum;
            switch(blockStr) {
                default:
                    BlockEnum = BlockEnum.Default;
                    break;
            }
            return BlockEnum;
        }
    }
}