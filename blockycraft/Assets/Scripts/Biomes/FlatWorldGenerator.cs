using UnityEngine;

namespace Assets.Scripts.Biomes
{
    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Flat")]
    public sealed class FlatWorldGenerator : WorldGenerator
    {
        public override BlockChunk Generate(Biome biome, int chunkX, int chunkZ)
        {
            var chunk = new BlockChunk(chunkX, chunkZ);
            var iterator = chunk.GetIterator();
            foreach (var (x, y, z) in iterator)
            {
                var idx = y % biome.Blocks.Length;
                chunk.Blocks[x, y, z] = biome.Blocks[idx].Type;
            }
            return chunk;
        }
    }
}
