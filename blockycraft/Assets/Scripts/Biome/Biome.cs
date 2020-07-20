using UnityEngine;
using Assets.Scripts.Biomes;

[CreateAssetMenu(fileName = "Biome", menuName = "Blockycraft/Biome")]
public sealed class Biome : ScriptableObject
{
    [Header("Descriptors")]
    public string Name;

    [Header("Properties")]
    public int GroundHeight;
    public int Height;

    [Header("Generator")]
    public WorldGenerator Generator;

    [Header("Composition")]
    public BiomeBlocks[] Blocks;

    public Biome() {
        GroundHeight = 1;
        Height = byte.MaxValue;
    }
}