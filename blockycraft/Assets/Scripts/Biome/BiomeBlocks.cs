namespace Assets.Scripts.Biome
{
    [System.Serializable]
    public sealed class BiomeBlocks
    {
        public BlockType Type;
        public int minHeight;
        public int maxHeight;
        public float noiseOffset;
        public float scale;
        public float threshold;
    }
}