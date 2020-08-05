using Assets.Scripts.World;
using Assets.Scripts.World.Chunk;
using UnityEngine;

namespace Assets.Scripts.Biome.Generator
{
    [CreateAssetMenu(fileName = "Biome", menuName = "Blockycraft/Biomes/Perlin")]
    public sealed class PerlinWorldGenerator : Biome
    {
        [Header("Composition")]
        public BlockType Bedrock;
        public BlockType Dirt;
        public BlockType Grass;
        public PerlinWorldGenerator.Block[] Blocks;

        [Header("Generation")]
        public int GroundHeight;
        public int Height;
        public float Scale;

        public override ChunkBlocks Generate(Vector3Int coordinate)
        {
            var chunk = new ChunkBlocks(coordinate.x, coordinate.y, coordinate.z, WorldComponent.SIZE);
            var iterator = chunk.GetIterator();
            foreach (var coord in iterator)
            {
                var x = coordinate.x * WorldComponent.SIZE + coord.x;
                var y = coordinate.y * WorldComponent.SIZE + coord.y;
                var z = coordinate.z * WorldComponent.SIZE + coord.z;

                var sample2d = MathHelper.Perlin2DSample(x, z, WorldComponent.SIZE, 0, Scale);
                var noise = Mathf.PerlinNoise(sample2d.x, sample2d.y);

                var terrainHeight = Mathf.FloorToInt(Height * noise) + GroundHeight;
                BlockType type = Bedrock;

                if (y == terrainHeight)
                    type = Grass;
                else if (y < terrainHeight && y > terrainHeight - 4)
                    type = Dirt;
                else if (y > terrainHeight)
                    type = Air;
                else
                {
                    foreach (var biomeBlock in Blocks)
                    {
                        if (y > biomeBlock.minHeight && y < biomeBlock.maxHeight)
                        {
                            var sample3d = MathHelper.Perlin3DSample(x, y, z, biomeBlock.noiseOffset, biomeBlock.scale);
                            if (MathHelper.Get3DPerlin(sample3d.x, sample3d.y, sample3d.z) > biomeBlock.threshold)
                                type = biomeBlock.Type;
                        }
                    }
                }
                chunk.Blocks[coord.x, coord.y, coord.z] = type;
            }
            return chunk;
        }

        [System.Serializable]
        public sealed class Block
        {
            public BlockType Type;
            public int minHeight;
            public int maxHeight;
            public float noiseOffset;
            public float scale;
            public float threshold;
        }
    }

}