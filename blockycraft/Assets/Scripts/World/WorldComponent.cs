using Assets.Scripts.World.Chunk;
using UnityEngine;

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

        public static string Key(int x, int y, int z)
        {
            return $"{x}:{y}:{z}";
        }

        public void Ping(Vector3Int position)
        {
            Chunks.Ping(position, radius, v => {
                var biome = biomes[(int)(Random.value * (biomes.Length))];
                return biome.Generator.Generate(biome, v);
            });
        }
    }
}
