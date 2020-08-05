using Assets.Scripts.World.Chunk;
using UnityEngine;

namespace Assets.Scripts.Biome
{
    public abstract class Biome : ScriptableObject
    {
        [Header("Mapping")]
        [Tooltip("Biomes that can be transitioned to from this biome.")]
        public Biome.Biome[] Transitions;

        public abstract ChunkBlocks Generate(Vector3Int coordinate);
    }
}