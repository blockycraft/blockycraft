public sealed class BlockChunk
{
    public const int SIZE = 8;
    public int Width => Blocks.GetLength(0);
    public int Length => Blocks.GetLength(1);
    public int Depth => Blocks.GetLength(2);
    public BlockType[,,] Blocks { get; }
    public int X { get; set; }
    public int Z { get; set; }

    public BlockChunk(int x, int z)
    {
        Blocks = new BlockType[SIZE, SIZE, SIZE];
        X = x;
        Z = z;
    }

    public static (int x, int y, int z) GetDirection(int x, int y, int z, BlockFace face)
    {
        switch (face)
        {
            case BlockFace.Back: return (x - 1, y, z);
            case BlockFace.Front: return (x + 1, y, z);
            case BlockFace.Left: return (x, y, z + 1);
            case BlockFace.Right: return (x, y, z - 1);
            case BlockFace.Top: return (x, y + 1, z);
            case BlockFace.Bottom: return (x, y - 1, z);
            default: return (x, y, z);
        }
    }

    public bool WithinBounds(int x, int y, int z)
    {
        return x < 0 || x >= Blocks.GetLength(0) ||
                   y < 0 || y >= Blocks.GetLength(1) ||
                   z < 0 || z >= Blocks.GetLength(2);
    }

    public BlockType GetNeighbour(int x, int y, int z, BlockFace face)
    {
        if (!WithinBounds(x, y, z))
        {
            throw new System.IndexOutOfRangeException();
        }

        var (nx, ny, nz) = GetDirection(x, y, z, face);
        return Blocks[nx, ny, nz];
    }

    public Iterator3D GetIterator()
    {
        return new Iterator3D(Width, Length, Depth);
    }
}
