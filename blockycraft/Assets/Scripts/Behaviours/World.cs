using Blockycraft;
using Blockycraft.Biome;
using Blockycraft.World;
using Blockycraft.World.Chunk;
using UnityEngine;

public sealed class World : MonoBehaviour
{
    public const int DRAW_HEIGHT = 4;
    public const int DRAW_DISTANCE = DRAW_HEIGHT * 2;
    public WorldComponent component;
    public Material material;
    public ChunkGenerator start;
    private System3D<Chunk> chunks;
    private ChunkFactory factory;

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

    public void Set(Vector3 target, BlockType type)
    {
        var coord = MathHelper.Anchor(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y), Mathf.FloorToInt(target.z), WorldComponent.SIZE);
        var block = MathHelper.Wrap(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y), Mathf.FloorToInt(target.z), WorldComponent.SIZE);
        if (!chunks.TryGet(ref coord, out Chunk chunk))
        {
            return;
        }

        chunk.Edit(block, type);
    }

    public (Vector3 lastPos, Vector3 pos, BlockType type) Detect(Vector3 position, Vector3 forward, float increment = 0.1f, float reach = 5f)
    {
        float step = increment;
        Vector3 lastPos = new Vector3();
        while (step < reach)
        {
            Vector3 pos = position + (forward * step);
            var type = component.GetBlock(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
            if (type != null && type.isVisible)
            {
                pos = new Vector3(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
                return (lastPos, pos, type);
            }
            lastPos = new Vector3(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
            step += increment;
        }
        return (Vector3.zero, Vector3.zero, null);
    }

    private void Start()
    {
        component = new WorldComponent(DRAW_DISTANCE * 2, start);
        chunks = new System3D<Chunk>();
        factory = new ChunkFactory();

        Ping(Vector3Int.zero);
    }

    private void Update()
    {
        factory.Process();

        var processed = factory.Completed();
        if (!processed.IsEmpty)
        {
        }
    }
}