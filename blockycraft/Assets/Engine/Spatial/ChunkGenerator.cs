﻿using Blockycraft.World.Chunk;
using UnityEngine;

namespace Blockycraft.Biome
{
    public abstract class ChunkGenerator : ScriptableObject
    {
        [Header("Descriptors")]
        public string Name;

        [Header("Essentials")]
        public BlockType Air;

        [Header("Relations")]
        [Tooltip("Biomes that can be transitioned to from this biome.")]
        public ChunkGenerator[] Transitions;

        public abstract ChunkBlocks Generate(Vector3Int coordinate, ChunkBlocks chunk, int size);
    }
}