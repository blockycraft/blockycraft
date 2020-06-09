using System;
using System.Linq;
using UnityEngine;

public sealed class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    private VoxelBuilder builder;
    private BlockChunk chunk;

    static BlockType[] ReadBlockTypes()
    {
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
        chunk = BlockChunk.Assorted(blockTypes);
        var mesh = builder.Build(chunk, Vector3.zero);
        meshFilter.mesh = mesh;
    }
}
