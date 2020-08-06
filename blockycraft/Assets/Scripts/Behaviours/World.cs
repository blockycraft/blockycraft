using Blockycraft;
using Blockycraft.Biome;
using Blockycraft.World;
using Blockycraft.World.Chunk;
using UnityEngine;

public sealed class World : MonoBehaviour
{
    public const int SIZE = 8;
    public const int DRAW_HEIGHT = 4;
    public const int DRAW_DISTANCE = DRAW_HEIGHT * 2;
    public Material material;

    private System3D<Chunk> chunks;
    public System3D<ChunkBlocks> Chunks { get; private set; }
    private ChunkFactory factory;
    private int radius;

    public ChunkGenerator start;
    private ChunkGenerator current;

    public void Ping(Vector3Int center)
    {
        var biome = current;
        if (Random.value < 0.40f)
        {
            biome = current.Transitions[(int)(Random.value * (current.Transitions.Length))];
            current = biome;
        }

        Chunks.Ping(center, radius, v =>
        {
            var chunk = new ChunkBlocks(v.x, v.y, v.z, SIZE);
            return biome.Generate(v, chunk, SIZE);
        });

        chunks.Ping(center, DRAW_HEIGHT, (v) =>
        {
            var blocks = Chunks.Get(v.x, v.y, v.z);
            var mesh = ChunkFactory.Build(blocks);
            return Chunk.Create(blocks, material, v.x, v.y, v.z, gameObject, mesh);
        });
    }

    public BlockType GetBlock(int x, int y, int z)
    {
        var coord = MathHelper.Anchor(x, y, z, SIZE);
        var block = MathHelper.Wrap(x, y, z, SIZE);
        if (!Chunks.TryGet(ref coord, out ChunkBlocks blocks))
        {
            return null;
        }

        if (!blocks.TryGet(ref block, out BlockType type))
        {
            return null;
        }
        return type;
    }

    public static string Key(int x, int y, int z)
    {
        return $"{x}:{y}:{z}";
    }

    public void Set(Vector3 target, BlockType type)
    {
        var coord = MathHelper.Anchor(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y), Mathf.FloorToInt(target.z), SIZE);
        var block = MathHelper.Wrap(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y), Mathf.FloorToInt(target.z), SIZE);
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
            var type = GetBlock(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
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
        radius = DRAW_DISTANCE;
        chunks = new System3D<Chunk>();
        Chunks = new System3D<ChunkBlocks>();
        factory = new ChunkFactory();
        current = start;

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