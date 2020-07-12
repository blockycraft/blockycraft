using UnityEngine;

public sealed class Chunk
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public BlockChunk Blocks { get; set; }
    public Material Voxel { get; set; }
    public int X { get; set; }
    public int Z { get; set; }
    public GameObject gameObject { get; set; }
    public Vector3 Position { get; set; }

    Chunk() { }
    
    void Initialize()
    {
        gameObject = new GameObject();
        gameObject.transform.position = Position;
        gameObject.name = $"Chunk {X},{Z}";

        var builder = new VoxelBuilder();
        var mesh = builder.Build(Blocks, Vector3.zero);

        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Voxel;

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }

    public static Chunk Create(BlockChunk blocks, Material material, int x, int z, GameObject parent)
    {
        var chunk = new Chunk
        {
            Blocks = blocks,
            Voxel = material,
            X = x,
            Z = z,
            Position = x * Vector3.left * BlockChunk.SIZE + z * Vector3.forward * BlockChunk.SIZE
        };
        chunk.Initialize();
        chunk.gameObject.transform.SetParent(parent.transform);
        return chunk;
    }
}
