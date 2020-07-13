using UnityEngine;
using System;
using System.Linq;

public sealed class World : MonoBehaviour
{
    private const int CHUNK_COUNT = 8;
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

    static Biome ReadFlatBiome()
    {
        Biome result;
        try
        {
            result = (Biome)Resources.Load("Biomes/Flat", typeof(Biome));
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
        var biome = ReadFlatBiome();
        
        for (int x = 0; x < chunks.GetLength(0); x++)
        {
            for (int z = 0; z < chunks.GetLength(1); z++)
            {
                var blocks = WorldGenerator.Generate(biome);
                chunks[x, z] = Chunk.Create(blocks, material, x, z, gameObject);
            }
        }
    }
}
