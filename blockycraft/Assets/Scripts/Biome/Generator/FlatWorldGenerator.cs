using Assets.Scripts.World;
using Assets.Scripts.World.Chunk;
using UnityEngine;

namespace Assets.Scripts.Biome.Generator
{
    [CreateAssetMenu(fileName = "Generator", menuName = "Blockycraft/Generators/Flat")]
    public sealed class FlatWorldGenerator : Biome
    {
        [Header("Descriptors")]
        public string Name;

        [Header("Composition")]
        public BlockType Air;
        public BlockType Shelf;
        public BlockType Top;

        [Header("Generation")]
        public int GroundHeight;
        public float Probability;

        public override ChunkBlocks Generate(Vector3Int coordinate)
        {
            var air = Air;
            var chunk = new ChunkBlocks(coordinate.x, coordinate.y, coordinate.z, WorldComponent.SIZE);
            var iterator = chunk.GetIterator();
            foreach (var coord in iterator)
            {
                var worldY = coordinate.y * WorldComponent.SIZE + coord.y;
                if (worldY > GroundHeight)
                {
                    chunk.Blocks[coord.x, coord.y, coord.z] = air;
                }
                else if (worldY == GroundHeight)
                {
                    if (Random.value < Probability)
                    {
                        chunk.Blocks[coord.x, coord.y, coord.z] = air;
                    } else
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