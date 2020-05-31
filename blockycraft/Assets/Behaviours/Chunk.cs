using System.Collections.Generic;
using UnityEngine;

public sealed class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    private VoxelBuilder builder;
    private BlockType[,] blocks;
    public const int SIZE = 16;

    void Start()
    {
        builder = new VoxelBuilder();

        var grassBlock = Resources.Load<BlockType>("BlockTypes/Grass");
        if (grassBlock == null)
        {
            throw new System.Exception();
        }

        blocks = new BlockType[SIZE, SIZE];
        for (int x = 0; x < blocks.GetLength(0); x++)
            for (int y = 0; y < blocks.GetLength(1); y++)
            {
                blocks[x, y] = grassBlock;
            }

        var mesh = builder.Build(blocks, Vector3.zero);
        meshFilter.mesh = mesh;
    }
}
