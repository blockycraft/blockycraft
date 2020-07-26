namespace Assets.Scripts.World.Chunk
{
    public sealed class ChunkView
    {
        public bool[,,,] Visible { get; private set; }
        public bool[,,] Blocks { get; private set; }
        public int Count { get; private set; }

        public ChunkView(int length, int height, int depth, int faces)
        {
            Visible = new bool[length, height, depth, faces];
            Blocks = new bool[length, height, depth];
            Count = 0;
        }

        public void Void(int x, int y, int z)
        {
            Blocks[x, y, z] = false;
        }

        public void Increment()
        {
            Count++;
        }
    }
}