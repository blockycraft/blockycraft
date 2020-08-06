using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blockycraft.Scripts.World.Chunk
{
    public sealed class ChunkBlocks : IEnumerable<BlockType>
    {
        public int Length => Blocks.GetLength(0);
        public int Height => Blocks.GetLength(1);
        public int Depth => Blocks.GetLength(2);
        public BlockType[,,] Blocks { get; }
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public ChunkBlocks(int x, int y, int z, int size)
        {
            Blocks = new BlockType[size, size, size];
            X = x;
            Y = y;
            Z = z;
        }

        public bool TryGet(ref Vector3Int coord, out BlockType type)
        {
            if (coord.x < 0 || coord.x >= Length ||
                coord.y < 0 || coord.y >= Height ||
                coord.z < 0 || coord.z >= Depth)
            {
                type = null;
                return false;
            }
            type = Blocks[coord.x, coord.y, coord.z];
            return true;
        }

        public bool Contains(int x, int y, int z)
        {
            if (x < 0 || x >= Length ||
                y < 0 || y >= Height ||
                z < 0 || z >= Depth)
            {
                return false;
            }
            return true;
        }

        public Iterator3D GetIterator()
        {
            return new Iterator3D(Length, Height, Depth);
        }

        public IEnumerator<BlockType> GetEnumerator()
        {
            var iterator = GetIterator();
            foreach (var coord in iterator)
                yield return Blocks[coord.x, coord.y, coord.z];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}