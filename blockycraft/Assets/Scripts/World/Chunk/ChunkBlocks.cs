namespace Assets.Scripts.World.Chunk
{
    public sealed class ChunkBlocks
    {
        public int Width => Blocks.GetLength(0);
        public int Length => Blocks.GetLength(1);
        public int Depth => Blocks.GetLength(2);
        public BlockType[,,] Blocks { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public ChunkBlocks(int x, int y, int z, int size)
        {
            Blocks = new BlockType[size, size, size];
            X = x;
            Y = y;
            Z = z;
        }

        public Iterator3D GetIterator()
        {
            return new Iterator3D(Width, Length, Depth);
        }
    }
}