using System.Collections.Generic;
using UnityEngine;

public sealed class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    private VoxelBuilder builder;
    private List<BlockType> blocks;

    void Start()
    {
        builder = new VoxelBuilder();

        var grassBlock = Resources.Load<BlockType>("BlockTypes/Grass");
        if (grassBlock == null) {
            throw new System.Exception();
        }

        blocks = new List<BlockType>();
        for (int i = 0; i < 16; i++)
            blocks.Add(grassBlock);

        var mesh = builder.Build(blocks, Vector3.zero);
        meshFilter.mesh = mesh;
    }
}
