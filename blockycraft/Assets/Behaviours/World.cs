using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Geometry;

public sealed class World : MonoBehaviour
{
    public const int DRAW_HEIGHT = 4;
    public const int DRAW_DISTANCE = DRAW_HEIGHT * 2;
    private Dictionary<string, Chunk> chunks;
    public Material material;
    public Biome[] biomes;

    public void AddChunks(int centerX, int centerY, int centerZ)
    {
        var iterator = new Iterator3D(DRAW_DISTANCE, DRAW_HEIGHT, DRAW_DISTANCE);
        foreach(var (ix, iy, iz) in iterator) {
            var x = centerX + (ix - DRAW_HEIGHT);
            var y = centerY + (iy - DRAW_HEIGHT);
            var z = centerZ + (iz - DRAW_HEIGHT);

            var key = $"{x}:{y}:{z}";
            if (chunks.ContainsKey(key))
            {
                continue;
            }

            var biome = biomes[(int)(Random.value * (biomes.Length))];
            var generator = biome.Generator;
            var blocks = generator.Generate(biome, x, y, z);
            var chunkFab = ChunkFactory.CreateFromBlocks(blocks);
            chunks[key] = Chunk.Create(chunkFab.Blocks, material, x, y, z, gameObject, chunkFab.ToMesh());
        }
    }

    void Start()
    {
        chunks = new Dictionary<string, Chunk>();

        AddChunks(0, 0, 0);
    }
}
