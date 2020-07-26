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

            var iterator = new Iterator3D(length, height, depth);
            foreach(var coord in iterator)
            {
                Blocks[coord.x, coord.y, coord.z] = false;
                for (int f = 0; f < faces; f++)
                    Visible[coord.x, coord.y, coord.z, f] = false;
            }

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