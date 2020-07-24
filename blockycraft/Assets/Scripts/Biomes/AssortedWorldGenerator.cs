using UnityEngine;

namespace Assets.Scripts.Biomes
{

    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Assorted")]
    public sealed class AssortedWorldGenerator : WorldGenerator
    {
        public override BlockChunk Generate(Biome biome, int chunkX, int chunkY, int chunkZ)
        {
            var chunk = new BlockChunk(chunkX, chunkY, chunkZ);
            var iterator = chunk.GetIterator();
            foreach (var (x, y, z) in iterator)
            {
                var idx = (y * chunk.Width + x) % biome.Blocks.Length;
                chunk.Blocks[x, y, z] = biome.Blocks[idx].Type;
            }
            return chunk;
        }
    }
}
