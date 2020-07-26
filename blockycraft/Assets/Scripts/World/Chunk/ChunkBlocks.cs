using Assets.Scripts.Geometry;
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

        public static Vector3Int GetDirection(int x, int y, int z, VoxelFace face)
        {
            switch (face)
            {
                case VoxelFace.Back: return new Vector3Int(x - 1, y, z);
                case VoxelFace.Front: return new Vector3Int(x + 1, y, z);
                case VoxelFace.Left: return new Vector3Int(x, y, z - 1);
                case VoxelFace.Right: return new Vector3Int(x, y, z + 1);
                case VoxelFace.Top: return new Vector3Int(x, y + 1, z);
                case VoxelFace.Bottom: return new Vector3Int(x, y - 1, z);
                default: return new Vector3Int(x, y, z);
            }
        }

        public bool WithinBounds(int x, int y, int z)
        {
            return x < 0 || x >= Width || y < 0 || y >= Length || z < 0 || z >= Depth;
        }

        public BlockType GetNeighbour(int x, int y, int z, VoxelFace face)
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