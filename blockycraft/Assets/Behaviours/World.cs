using Assets.Scripts.Biome;
using Assets.Scripts.World;
using Assets.Scripts.World.Chunk;
using System.Collections.Generic;
using UnityEngine;

public sealed class World : MonoBehaviour
{
    public const int DRAW_HEIGHT = 4;
    public const int DRAW_DISTANCE = DRAW_HEIGHT * 2;
    private WorldComponent component;
    private Dictionary<string, Chunk> chunks;
    public Material material;
    public Biome[] biomes;
    
    public void Ping(Vector3Int center)
    {
        component.Ping(center);
        var iterator = new Iterator3D(DRAW_DISTANCE, DRAW_HEIGHT, DRAW_DISTANCE);
        foreach (var coord in iterator)
        {
            var x = center.x + (coord.x - DRAW_HEIGHT);
            var y = center.y + (coord.y - DRAW_HEIGHT);
            var z = center.z + (coord.z - DRAW_HEIGHT);

            var key = WorldComponent.Key(x, y, z);
            if (chunks.ContainsKey(key))
            {
                continue;
            }

            var blocks = component.Get(key);
            var mesh = ChunkFactory.Build(blocks);
            chunks[key] = Chunk.Create(blocks, material, x, y, z, gameObject, mesh);
        }
    }

    private void Start()
    {
        component = new WorldComponent(DRAW_DISTANCE * 2, biomes);
        chunks = new Dictionary<string, Chunk>();

        Ping(Vector3Int.zero);
    }
}