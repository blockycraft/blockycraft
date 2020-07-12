[System.Serializable]
public sealed class BiomeBlocks
{
    public BlockType Type;
    public int Minimum;
    public int Maximum;
    public float Threshold;

    public BiomeBlocks() {
        Minimum = 1;
        Maximum = byte.MaxValue;
        Threshold = 1.0f;
    }
}

