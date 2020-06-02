using System;
using System.Linq;
using UnityEngine;

public sealed class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    private VoxelBuilder builder;
    private BlockType[,] blocks;
    public const int SIZE = 16;

    static BlockType[] ReadBlockTypes() {
        BlockType[] result;
        try
        {
            result = Resources.LoadAll("BlockTypes/", typeof(BlockType)).Cast<BlockType>().ToArray();
        }
        catch (Exception e)
        {
            Debug.Log("Proper Method failed with the following exception: ");
            Debug.Log(e);
            throw e;
        }
        return result;
    }

    void Start()
    {
        var blockTypes = ReadBlockTypes();
        
        builder = new VoxelBuilder();
        blocks = new BlockType[SIZE, SIZE];
        for (int x = 0; x < blocks.GetLength(0); x++)
            for (int y = 0; y < blocks.GetLength(1); y++)
            {
                var idx = (y * blocks.GetLength(0) + x) % blockTypes.Length;
                blocks[x, y] = blockTypes[idx];
            }

        var mesh = builder.Build(blocks, Vector3.zero);
        meshFilter.mesh = mesh;
    }
}
