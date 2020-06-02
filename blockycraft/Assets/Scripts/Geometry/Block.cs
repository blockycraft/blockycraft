public sealed class Block
{
    public BlockType Type { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Block(int x, int y, int z, BlockType type)
    {
        X = x;
        Y = y;
        Z = z;
        Type = type;
    }
}
