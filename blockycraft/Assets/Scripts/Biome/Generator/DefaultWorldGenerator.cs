using Assets.Scripts.World.Chunk;
using UnityEngine;

namespace Assets.Scripts.Biome.Generator
{
    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Default")]
    public sealed class DefaultWorldGenerator : WorldGenerator
    {
        public int Index;

        public override BlockChunk Generate(Biome biome, Vector3Int coordinate)
        {
            var chunk = new BlockChunk(coordinate.x, coordinate.y, coordinate.z);
            var iterator = chunk.GetIterator();
            foreach (var coord in iterator)
                chunk.Blocks[coord.x, coord.y, coord.z] = biome.Blocks[Index].Type;

            return chunk;
        }
    }
}