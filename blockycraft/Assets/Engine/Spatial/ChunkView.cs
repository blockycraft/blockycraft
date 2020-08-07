using Blockycraft.Engine.Geometry;

namespace Blockycraft.World.Chunk
{
    public sealed class ChunkView
    {
        public bool[,,,] Faces { get; }
        public bool[,,] Blocks { get; }
        public int Count { get; private set; }

        public ChunkView(int length, int height, int depth)
        {
            Faces = new bool[length, height, depth, Voxel.NumberOfFaces];
            Blocks = new bool[length, height, depth];

            var iterator = new Iterator3D(length, height, depth);
            foreach (var coord in iterator)
            {
                Blocks[coord.x, coord.y, coord.z] = false;
                for (int f = 0; f < Voxel.NumberOfFaces; f++)
                    Faces[coord.x, coord.y, coord.z, f] = false;
            }
            Count = 0;
        }

        public void Increment()
        {
            Count++;
        }
    }
}