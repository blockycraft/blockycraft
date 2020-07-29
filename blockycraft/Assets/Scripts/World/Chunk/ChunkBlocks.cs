using UnityEngine;

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

        public bool TryGet(ref Vector3Int coord, out BlockType type)
        {
            if (coord.x < 0 || coord.x >= Blocks.GetLength(0) ||
                coord.y < 0 || coord.y >= Blocks.GetLength(1) ||
                coord.z < 0 || coord.z >= Blocks.GetLength(2))
            {
                type = null;
                return false;
            }
            type = Blocks[coord.x, coord.y, coord.z];
            return true;
        }

        public bool Contains(int x, int y, int z)
        {
            if (x < 0 || x >= Blocks.GetLength(0) ||
                y < 0 || y >= Blocks.GetLength(1) ||
                z < 0 || z >= Blocks.GetLength(2))
            {
                return false;
            }
            return true;
        }

        public Iterator3D GetIterator()
        {
            return new Iterator3D(Width, Length, Depth);
        }
    }
}