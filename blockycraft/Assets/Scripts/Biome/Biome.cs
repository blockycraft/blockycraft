using Assets.Scripts.Biome.Generator;
using UnityEngine;

namespace Assets.Scripts.Biome
{
    [CreateAssetMenu(fileName = "Biome", menuName = "Blockycraft/Biome")]
    public sealed class Biome : ScriptableObject
    {
        [Header("Descriptors")]
        public string Name;

        [Header("Generator")]
        public WorldGenerator Generator;

        [Header("Composition")]
        public BlockType Air;

        public int GroundHeight;
        public float Probability;
        public BiomeBlocks[] Blocks;
    }
}