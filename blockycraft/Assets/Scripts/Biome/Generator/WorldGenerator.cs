using Assets.Scripts.World.Chunk;
using UnityEngine;

namespace Assets.Scripts.Biome.Generator
{
    public abstract class WorldGenerator : ScriptableObject
    {
        public abstract ChunkBlocks Generate(Biome biome, Vector3Int coordinate);
    }
}