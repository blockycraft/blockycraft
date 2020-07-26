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
        public BiomeBlocks[] Blocks;
    }
}