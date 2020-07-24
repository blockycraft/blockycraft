using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Biomes
{
    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Flat")]
    public sealed class FlatWorldGenerator : WorldGenerator
    {
        public override BlockChunk Generate(Biome biome, int chunkX, int chunkZ)
        {
            var air = biome.Blocks.FirstOrDefault(p => !p.Type.isVisible);
            var chunk = new BlockChunk(chunkX, chunkZ);
            var iterator = chunk.GetIterator();
            foreach (var (x, y, z) in iterator)
            {
                if (air != null && y >= iterator.Height - 1 && Random.value < 0.15f) {
                    chunk.Blocks[x, y, z] = air.Type;
                } else {
                    var idx = y % biome.Blocks.Length;
                    chunk.Blocks[x, y, z] = biome.Blocks[idx].Type;
                }
            }
            return chunk;
        }
    }
}
