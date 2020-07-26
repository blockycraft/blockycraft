using Assets.Scripts.Geometry;
using UnityEngine;

namespace Assets.Scripts.World.Chunk
{
    public sealed class BlockChunk
    {
        public const int SIZE = 8;
        public int Width => Blocks.GetLength(0);
        public int Length => Blocks.GetLength(1);
        public int Depth => Blocks.GetLength(2);
        public BlockType[,,] Blocks { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public BlockChunk(int x, int y, int z)
        {
            Blocks = new BlockType[SIZE, SIZE, SIZE];
            X = x;
            Y = y;
            Z = z;
        }

        public BlockChunk(int x, int y, int z, int size)
        {
            Blocks = new BlockType[size, size, size];
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3Int GetDirection(int x, int y, int z, BlockFace face)
        {
            switch (face)
            {
                case BlockFace.Back: return new Vector3Int(x - 1, y, z);
                case BlockFace.Front: return new Vector3Int(x + 1, y, z);
                case BlockFace.Left: return new Vector3Int(x, y, z - 1);
                case BlockFace.Right: return new Vector3Int(x, y, z + 1);
                case BlockFace.Top: return new Vector3Int(x, y + 1, z);
                case BlockFace.Bottom: return new Vector3Int(x, y - 1, z);
                default: return new Vector3Int(x, y, z);
            }
        }

        public bool WithinBounds(int x, int y, int z)
        {
            return x < 0 || x >= Width || y < 0 || y >= Length || z < 0 || z >= Depth;
        }

        public BlockType GetNeighbour(int x, int y, int z, BlockFace face)
        {
            if (!WithinBounds(x, y, z))
            {
                throw new System.IndexOutOfRangeException();
            }

            var neighbour = GetDirection(x, y, z, face);
            return Blocks[neighbour.x, neighbour.y, neighbour.z];
        }

        public Iterator3D GetIterator()
        {
            return new Iterator3D(Width, Length, Depth);
        }
    }
}