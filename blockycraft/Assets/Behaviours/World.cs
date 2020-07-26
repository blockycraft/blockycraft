using Assets.Scripts.Biome;
using Assets.Scripts.World.Chunk;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (var coord in iterator)
        {
            var x = centerX + (coord.x - DRAW_HEIGHT);
            var y = centerY + (coord.y - DRAW_HEIGHT);
            var z = centerZ + (coord.z - DRAW_HEIGHT);

            var key = $"{x}:{y}:{z}";
            if (chunks.ContainsKey(key))
            {
                continue;
            }

            var biome = biomes[(int)(Random.value * (biomes.Length))];
            var generator = biome.Generator;
            var blocks = generator.Generate(biome, new Vector3Int(x, y, z));
            var mesh = ChunkFactory.Build(blocks);
            chunks[key] = Chunk.Create(blocks, material, x, y, z, gameObject, mesh);
        }
    }

    private void Start()
    {
        chunks = new Dictionary<string, Chunk>();

        AddChunks(0, 0, 0);
    }
}