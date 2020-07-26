namespace Assets.Scripts.World.Chunk
{
    public sealed class ChunkView
    {
        public bool[,,,] Visibility { get; private set; }
        public bool[,,] Blocks { get; private set; }
        public int Count { get; private set; }

        public ChunkView(int length, int height, int depth, int faces)
        {
            Visibility = new bool[length, height, depth, faces];
            Blocks = new bool[length, height, depth];
            Count = 0;
        }
        
        public void Increment()
        {
            Count++;
        }
    }
}