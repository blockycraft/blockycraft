using Assets.Scripts;
using Assets.Scripts.World;
using Assets.Scripts.World.Chunk;
using UnityEngine;

public sealed class Chunk
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public Mesh Mesh { get; set; }
    public ChunkBlocks Blocks { get; set; }
    public Material Voxel { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public GameObject gameObject { get; set; }
    public Vector3 Position { get; set; }

    private Chunk()
    { }

    private void Initialize()
    {
        gameObject = new GameObject();
        gameObject.transform.position = Position;
        gameObject.name = $"Chunk {X},{Y},{Z}";

        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Voxel;

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = Mesh;
    }

    public void Edit(Vector3Int coord, BlockType type)
    {
        if (!Blocks.Contains(coord.x, coord.y, coord.z) || Blocks.Blocks[coord.x, coord.y, coord.z] == type)
        {
            return;
        }

        Blocks.Blocks[coord.x, coord.y, coord.z] = type;

        var mesh = ChunkFactory.Build(Blocks);
        Mesh = mesh;
        meshFilter.mesh = mesh;
    }

    public static Chunk Create(ChunkBlocks blocks, Material material, int x, int y, int z, GameObject parent, Mesh mesh)
    {
        var chunk = new Chunk
        {
            Blocks = blocks,
            Voxel = material,
            X = x,
            Y = y,
            Z = z,
            Mesh = mesh,
            Position = x * Vector3.right * WorldComponent.SIZE + z * Vector3.forward * WorldComponent.SIZE + y * Vector3.up * WorldComponent.SIZE
        };
        chunk.Initialize();

        chunk.gameObject.transform.SetParent(parent.transform);
        return chunk;
    }
}