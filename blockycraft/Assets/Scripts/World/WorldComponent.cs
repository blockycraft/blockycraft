using Assets.Scripts.World.Chunk;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.World
{
    public sealed class WorldComponent
    {
        public const int SIZE = 8;
        public Biome.Biome[] biomes;

        public System3D<ChunkBlocks> Chunks { get; }
        private readonly int radius;

        public WorldComponent(int circumference, Biome.Biome[] biomes)
        {
            Chunks = new System3D<ChunkBlocks>();
            radius = circumference / 2;
            this.biomes = biomes;
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
            Chunks.Ping(position, radius, v =>
            {
                var biome = biomes[(int)(Random.value * (biomes.Length))];
                return biome.Generator.Generate(biome, v);
            });
        }
    }
}
