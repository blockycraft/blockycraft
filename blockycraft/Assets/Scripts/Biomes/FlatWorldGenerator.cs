using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Biomes
{
    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Flat")]
    public sealed class FlatWorldGenerator : WorldGenerator
    {
        public override BlockChunk Generate(Biome biome, int chunkX, int chunkY, int chunkZ)
        {
            var air = biome.Blocks.FirstOrDefault(p => !p.Type.isVisible);
            var chunk = new BlockChunk(chunkX, chunkY, chunkZ);
            var iterator = chunk.GetIterator();
            foreach (var coord in iterator)
            {
                if ((air != null && coord.y >= iterator.Height - 1 && Random.value < 0.15f) 
                    || chunkY*BlockChunk.SIZE+ coord.y >= BlockChunk.SIZE) {
                    chunk.Blocks[coord.x, coord.y, coord.z] = air.Type;
                } else {
                    var idx = coord.y % biome.Blocks.Length;
                    chunk.Blocks[coord.x, coord.y, coord.z] = biome.Blocks[idx].Type;
                }
            }
            return chunk;
        }
    }
}
