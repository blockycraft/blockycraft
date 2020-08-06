using Blockycraft.Scripts.World.Chunk;
using UnityEngine;

namespace Blockycraft.Scripts.World
{
    public sealed class WorldComponent
    {
        public const int SIZE = 8;
        

        public System3D<ChunkBlocks> Chunks { get; }
        private readonly int radius;
        private readonly Biome.Biome starter;
        private readonly Biome.Biome current;

        public WorldComponent(int circumference, Biome.Biome start)
        {
            Chunks = new System3D<ChunkBlocks>();
            radius = circumference / 2;
            starter = start;
            current = start;
        }

        public BlockType GetBlock(int x, int y, int z)
        {
            var coord = MathHelper.Anchor(x, y, z, SIZE);
            var block = MathHelper.Wrap(x, y, z, SIZE);
            if (!Chunks.TryGet(ref coord, out ChunkBlocks blocks))
            {
                return null;
            }

            if (!blocks.TryGet(ref block, out BlockType type))
            {
                return null;
            }
            return type;
        }

        public static string Key(int x, int y, int z)
        {
            return $"{x}:{y}:{z}";
        }

        public void Ping(Vector3Int position)
        {
            var biome = current;
            if (Random.value < 0.40f)
            {
                biome = current.Transitions[(int)(Random.value * (current.Transitions.Length))];
            }

            Chunks.Ping(position, radius, v =>
            {
                return biome.Generate(v);
            });
        }
    }
}