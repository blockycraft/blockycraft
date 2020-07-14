using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public sealed class World : MonoBehaviour
{
    private const int CHUNK_COUNT = 8;
    public const int DRAW_DISTANCE = 8;
    private Dictionary<string, Chunk> chunks;
    public Material material;
    private Biome biome;

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

    public void AddChunks(int x, int z)
    {
        for (var i = -DRAW_DISTANCE; i <= DRAW_DISTANCE; i++)
        {
            for (var j = -DRAW_DISTANCE; j <= DRAW_DISTANCE; j++)
            {
                AddChunk(x + i, z + j);
            }
        }
    }

    public void AddChunk(int x, int z)
    {
        var key = $"{x}:{z}";
        if (chunks.ContainsKey(key))
        {
            return;
        }

        var blocks = WorldGenerator.Generate(biome);
        chunks[key] = Chunk.Create(blocks, material, x, z, gameObject);
    }

    void Start()
    {
        chunks = new Dictionary<string, Chunk>();
        biome = ReadFlatBiome();

        for (int x = 0; x < CHUNK_COUNT; x++)
        {
            for (int z = 0; z < CHUNK_COUNT; z++)
            {
                AddChunk(x, z);
            }
        }
    }
}
