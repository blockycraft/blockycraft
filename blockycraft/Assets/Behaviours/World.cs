using UnityEngine;
using System;
using System.Linq;

public sealed class World : MonoBehaviour
{
    private const int CHUNK_COUNT = 4;
    private Chunk[,] chunks = new Chunk[CHUNK_COUNT, CHUNK_COUNT];
    public Material material;

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
        
        for (int x = 0; x < chunks.GetLength(0); x++)
        {
            for (int z = 0; z < chunks.GetLength(1); z++)
            {
                var chunk = gameObject.AddComponent<Chunk>();
                chunk.Blocks =  BlockChunk.Assorted(blockTypes);
                chunk.Voxel = material;
                chunk.X = x;
                chunk.Z = z;
                chunk.Position = x * Vector3.left * BlockChunk.SIZE + z * Vector3.forward * BlockChunk.SIZE;
                
                chunks[x, z] = chunk;
            }
        }
    }
}
