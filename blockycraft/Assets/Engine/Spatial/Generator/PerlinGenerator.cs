﻿using Blockycraft.Scripts.World.Chunk;
using UnityEngine;

namespace Blockycraft.Scripts.Biome.Generator
{
    [CreateAssetMenu(fileName = "Biome", menuName = "Blockycraft/Biomes/Perlin")]
    public sealed class PerlinGenerator : ChunkGenerator
    {
        [Header("Composition")]
        public BlockType Bedrock;

        public BlockType Dirt;
        public BlockType Grass;
        public Block[] Blocks;

        [Header("Generation")]
        public int GroundHeight;

        public int Height;
        public float Scale;

        public override ChunkBlocks Generate(Vector3Int coordinate, int size)
        {
            var chunk = new ChunkBlocks(coordinate.x, coordinate.y, coordinate.z, size);
            chunk.Biome = this;
            var iterator = chunk.GetIterator();
            foreach (var coord in iterator)
            {
                var x = coordinate.x * size + coord.x;
                var y = coordinate.y * size + coord.y;
                var z = coordinate.z * size + coord.z;

                var sample2d = MathHelper.Perlin2DSample(x, z, size, 0, Scale);
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