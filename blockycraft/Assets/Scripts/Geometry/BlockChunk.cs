using System.Collections.Generic;

public sealed class BlockChunk
{
    public const int SIZE = 8;
    public int Width => Blocks.GetLength(0);
    public int Length => Blocks.GetLength(1);
    public int Depth => Blocks.GetLength(2);
    public BlockType[,,] Blocks { get; }

    public BlockChunk()
    {
        Blocks = new BlockType[SIZE, SIZE, SIZE];
    }

    public Iterator3D GetIterator()
    {
        return new Iterator3D(Width, Length, Depth);
    }
}
