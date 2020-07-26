using Assets.Scripts.World;
using Assets.Scripts.World.Chunk;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Biome.Generator
{
    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Flat")]
    public sealed class FlatWorldGenerator : WorldGenerator
    {
        public override ChunkBlocks Generate(Biome biome, Vector3Int coordinate)
        {
            var air = biome.Air;
            var chunk = new ChunkBlocks(coordinate.x, coordinate.y, coordinate.z, WorldComponent.SIZE);
            var iterator = chunk.GetIterator();
            foreach (var coord in iterator)
            {
                var worldY = coordinate.y * WorldComponent.SIZE + coord.y;
                if (worldY >= biome.GroundHeight)
                {
                    chunk.Blocks[coord.x, coord.y, coord.z] = air;
                }
                else if(worldY == biome.GroundHeight - 1 && Random.value < biome.Probability)
                {
                    chunk.Blocks[coord.x, coord.y, coord.z] = air;
                }
                else
                {
                    var idx = coord.y % biome.Blocks.Length;
                    chunk.Blocks[coord.x, coord.y, coord.z] = biome.Blocks[idx].Type;
                }
            }
            return chunk;
        }
    }
}