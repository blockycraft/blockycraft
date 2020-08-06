using Blockycraft.Scripts.World;
using Blockycraft.Scripts.World.Chunk;
using UnityEngine;

namespace Blockycraft.Scripts.Biome.Generator
{
    [CreateAssetMenu(fileName = "Biome", menuName = "Blockycraft/Biomes/Flat")]
    public sealed class FlatGenerator : ChunkGenerator
    {
        [Header("Composition")]
        public BlockType Shelf;
        public BlockType Top;

        [Header("Generation")]
        public int GroundHeight;

        public float Probability;

        public override ChunkBlocks Generate(Vector3Int coordinate, int size)
        {
            var air = Air;
            var chunk = new ChunkBlocks(coordinate.x, coordinate.y, coordinate.z, size);
            chunk.Biome = this;
            var iterator = chunk.GetIterator();
            foreach (var coord in iterator)
            {
                var worldY = coordinate.y * size + coord.y;
                if (worldY > GroundHeight)
                {
                    chunk.Blocks[coord.x, coord.y, coord.z] = air;
                }
                else if (worldY == GroundHeight)
                {
                    if (Random.value < Probability)
                    {
                        chunk.Blocks[coord.x, coord.y, coord.z] = air;
                    }
                    else
                    {
                        chunk.Blocks[coord.x, coord.y, coord.z] = Top;
                    }
                }
                else
                {
                    chunk.Blocks[coord.x, coord.y, coord.z] = Shelf;
                }
            }
            return chunk;
        }
    }
}