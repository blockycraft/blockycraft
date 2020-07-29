using Assets.Scripts.Geometry;

namespace Assets.Scripts.World.Chunk
{
    public sealed class ChunkView
    {
        public bool[,,,] Faces { get; }
        public bool[,,] Blocks { get; }
        public int Count { get; private set; }

        public ChunkView(int length, int height, int depth, int faces)
        {
            Faces = new bool[length, height, depth, faces];
            Blocks = new bool[length, height, depth];

            var iterator = new Iterator3D(length, height, depth);
            foreach(var coord in iterator)
            {
                Blocks[coord.x, coord.y, coord.z] = false;
                for (int f = 0; f < faces; f++)
                    Faces[coord.x, coord.y, coord.z, f] = false;
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