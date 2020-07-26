using Assets.Scripts.World.Chunk;
using UnityEngine;

namespace Assets.Scripts.Biome.Generator
{
    public abstract class WorldGenerator : ScriptableObject
    {
        public abstract BlockChunk Generate(Biome biome, int chunkX, int chunkY, int chunkZ);
    }
}