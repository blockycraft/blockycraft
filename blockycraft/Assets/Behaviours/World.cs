using UnityEngine;
using System;
using System.Collections.Generic;
using Assets.Scripts.Geometry;

public sealed class World : MonoBehaviour
{
    public const int DRAW_DISTANCE = 4;
    private Dictionary<string, Chunk> chunks;
    public Material material;
    private Biome biome;

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

    public void AddChunks(int centerX, int centerZ)
    {
        var tasks = new List<ChunkFab>();
        for (var i = -DRAW_DISTANCE; i <= DRAW_DISTANCE; i++)
        {
            for (var j = -DRAW_DISTANCE; j <= DRAW_DISTANCE; j++)
            {
                var x = centerX + i;
                var z = centerZ + j;

                var key = $"{x}:{z}";
                if (chunks.ContainsKey(key))
                {
                    continue;
                }
                

                var blocks = WorldGenerator.Generate(biome, x, z);
                tasks.Add(ChunkFactory.CreateFromBlocks(blocks));
            }
        }


        if (tasks.Count == 0) { return; }
        
        foreach(var chunkFab in tasks)
        {
            var key = $"{chunkFab.Blocks.X}:{chunkFab.Blocks.Z}";
            chunks[key] = Chunk.Create(chunkFab.Blocks, material, chunkFab.Blocks.X, chunkFab.Blocks.Z, gameObject, chunkFab.ToMesh());
        }
    }

    void Start()
    {
        chunks = new Dictionary<string, Chunk>();
        biome = ReadFlatBiome();

        AddChunks(0, 0);
    }
}
