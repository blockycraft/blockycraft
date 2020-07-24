using UnityEngine;

namespace Assets.Scripts.Biomes
{

    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Default")]
    public sealed class DefaultWorldGenerator : WorldGenerator
    {
        public int Index;

        public override BlockChunk Generate(Biome biome, int chunkX, int chunkY, int chunkZ)
        {
            var chunk = new BlockChunk(chunkX, chunkY, chunkZ);
            var iterator = chunk.GetIterator();
            foreach (var (x, y, z) in iterator)
                chunk.Blocks[x, y, z] = biome.Blocks[Index].Type;

            return chunk;
        }
    }
}
