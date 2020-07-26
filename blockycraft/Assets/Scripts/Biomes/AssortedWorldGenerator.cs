﻿using UnityEngine;

namespace Assets.Scripts.Biomes
{

    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Assorted")]
    public sealed class AssortedWorldGenerator : WorldGenerator
    {
        public override BlockChunk Generate(Biome biome, int chunkX, int chunkY, int chunkZ)
        {
            var chunk = new BlockChunk(chunkX, chunkY, chunkZ);
            var iterator = chunk.GetIterator();
            foreach (var coord in iterator)
            {
                var idx = (coord.y * chunk.Width + coord.x) % biome.Blocks.Length;
                chunk.Blocks[coord.x, coord.y, coord.z] = biome.Blocks[idx].Type;
            }
            return chunk;
        }
    }
}
