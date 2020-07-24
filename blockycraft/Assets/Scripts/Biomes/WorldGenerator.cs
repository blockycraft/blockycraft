using UnityEngine;

namespace Assets.Scripts.Biomes
{
    public abstract class WorldGenerator : ScriptableObject
    {
        public abstract BlockChunk Generate(Biome biome, int chunkX, int chunkY, int chunkZ);
    }
}
