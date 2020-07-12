using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Biome", menuName = "Blockycraft/Biome")]
public sealed class Biome : ScriptableObject
{
    [Header("Descriptors")]
    public string Name;

    [Header("Properties")]
    public int GroundHeight;
    public int Height;

    [Header("Composition")]
    public List<BiomeBlocks> Blocks;

    public Biome() {
        GroundHeight = 1;
        Height = byte.MaxValue;
        Blocks = new List<BiomeBlocks>();
    }
}
