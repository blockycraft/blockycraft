using Assets.Scripts.World.Chunk;
using UnityEngine;

namespace Assets.Scripts.Biome
{
    public abstract class Biome : ScriptableObject
    {
        public abstract ChunkBlocks Generate(Vector3Int coordinate);
    }
}