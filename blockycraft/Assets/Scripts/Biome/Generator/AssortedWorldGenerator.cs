using Assets.Scripts.World;
using Assets.Scripts.World.Chunk;
using UnityEngine;

namespace Assets.Scripts.Biome.Generator
{
    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Assorted")]
    public sealed class AssortedWorldGenerator : WorldGenerator
    {
        public override ChunkBlocks Generate(Biome biome, Vector3Int coordinate)
        {
            var chunk = new ChunkBlocks(coordinate.x, coordinate.y, coordinate.z, WorldComponent.SIZE);
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