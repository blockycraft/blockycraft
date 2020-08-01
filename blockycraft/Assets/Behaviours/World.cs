using Assets.Scripts.Biome;
using Assets.Scripts.World;
using Assets.Scripts.World.Chunk;
using UnityEngine;

public sealed class World : MonoBehaviour
{
    public const int DRAW_HEIGHT = 4;
    public const int DRAW_DISTANCE = DRAW_HEIGHT * 2;
    public WorldComponent component;
    private System3D<Chunk> chunks;
    public Material material;
    public Biome[] biomes;

    public void Ping(Vector3Int center)
    {
        component.Ping(center);
        chunks.Ping(center, DRAW_HEIGHT, (v) =>
        {
            var blocks = component.Chunks.Get(v.x, v.y, v.z);
            var mesh = ChunkFactory.Build(blocks);
            return Chunk.Create(blocks, material, v.x, v.y, v.z, gameObject, mesh);
        });
    }

    private void Start()
    {
        component = new WorldComponent(DRAW_DISTANCE * 2, biomes);
        chunks = new System3D<Chunk>();

        Ping(Vector3Int.zero);
    }
}