using Assets.Scripts.World.Chunk;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
    public sealed class WorldComponent
    {
        public const int SIZE = 8;
        public Biome.Biome[] biomes;

        private Dictionary<string, ChunkBlocks> chunks;
        private int circum;
        private int radius;

        public WorldComponent(int circumference, Biome.Biome[] biomes)
        {
            chunks = new Dictionary<string, ChunkBlocks>();
            circum = circumference;
            radius = circumference / 2;

            this.biomes = biomes;
        }

        public static string Key(int x, int y, int z)
        {
            return $"{x}:{y}:{z}";
        }

        public ChunkBlocks Get(string key)
        {
            return chunks[key];
        }

        public bool TryGet(ref Vector3Int position, out BlockType type)
        {
            int x = position.x / SIZE;
            int y = position.y / SIZE;
            int z = position.z / SIZE;

            var key = Key(x, y, z);
            if (!chunks.ContainsKey(key)) {
                type = null;
                return false;
            }

            var chunk = chunks[key];
            type = chunk.Blocks[x - x*SIZE, y - y*SIZE, z - z*SIZE];
            return true;
        }

        public void Ping(Vector3Int position)
        {
            var iterator = new Iterator3D(circum);
            foreach (var coord in iterator)
            {
                var x = position.x + (coord.x - radius);
                var y = position.y + (coord.y - radius);
                var z = position.z + (coord.z - radius);
                var adjusted = new Vector3Int(x, y, z);

                var key = Key(x, y, z);
                if (chunks.ContainsKey(key))
                {
                    continue;
                }

                var biome = biomes[(int)(Random.value * (biomes.Length))];
                chunks[key] = biome.Generator.Generate(biome, adjusted);
            }
        }
    }
}
