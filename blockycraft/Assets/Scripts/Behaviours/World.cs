using Blockycraft;
using Blockycraft.Biome;
using Blockycraft.World;
using Blockycraft.World.Chunk;
using UnityEngine;

public sealed class World : MonoBehaviour
{
    public const int SIZE = 16;
    public const int STARTUP_PROCESSED_CHUNKS = 16;
    public Material material;

    private Space3D<Chunk> chunks;
    private Space3D<ChunkBlocks> chunkBlocks;
    private Space3D<ChunkGenerator> generators;
    private ChunkFactory factory;
    private Vector3Int radius;

    public Player player;
    public ChunkGenerator start;
    private ChunkGenerator current;

    private void Start()
    {
        radius = new Vector3Int(16, 3, 16);
        chunks = new Space3D<Chunk>();
        generators = new Space3D<ChunkGenerator>();
        chunkBlocks = new Space3D<ChunkBlocks>();
        factory = new ChunkFactory();
        current = start;

        Ping(player.Chunk());
        for (int i = 0; i < STARTUP_PROCESSED_CHUNKS; i++) factory.Process(player.Chunk());
    }

    private void Update()
    {
        factory.Process(player.Chunk());

        UpdateChunks();

        Ping(player.Chunk());
    }

    private void UpdateChunks()
    {
        var processed = factory.Completed();
        if (!processed.IsEmpty)
        {
            foreach (var entity in processed)
            {
                var coord = entity.Coordinate;
                var mesh = entity.Element;
                var blocks = chunkBlocks.Get(coord);
                var chunk = Chunk.Create(blocks, material, coord.x, coord.y, coord.z, gameObject, mesh);
                chunks.Set(coord, chunk);
            }
        }
    }

    public void Ping(Vector3Int position)
    {
        var biome = current;
        if (Random.value < 0.40f)
        {
            biome = current.Transitions[(int)(Random.value * (current.Transitions.Length))];
            current = biome;
        }

        //generators

        chunkBlocks.Ping(position, radius, v =>
        {
            var chunk = new ChunkBlocks(v.x, v.y, v.z, SIZE);
            var result = biome.Generate(v, chunk, SIZE);
            factory.Enqueue(result);
            return result;
        });
    }

    public BlockType GetBlock(int x, int y, int z)
    {
        var coord = MathHelper.Anchor(x, y, z, SIZE);
        var block = MathHelper.Wrap(x, y, z, SIZE);
        if (!chunkBlocks.TryGet(ref coord, out ChunkBlocks blocks))
        {
            return null;
        }

        if (!blocks.TryGet(ref block, out BlockType type))
        {
            return null;
        }
        return type;
    }

    public bool HasBlock(float x, float y, float z)
    {
        return HasBlock(Mathf.RoundToInt(x), Mathf.RoundToInt(y), Mathf.RoundToInt(z));
    }

    public bool HasBlock(int x, int y, int z)
    {
        var type = GetBlock(x, y, z);
        return type != null && type.isVisible;
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
}