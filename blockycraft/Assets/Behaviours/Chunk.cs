using Assets.Scripts.World.Chunk;
using UnityEngine;

public sealed class Chunk
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public Mesh Mesh { get; set; }
    public BlockChunk Blocks { get; set; }
    public Material Voxel { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public GameObject gameObject { get; set; }
    public Vector3 Position { get; set; }

    Chunk() { }
    
    void Initialize()
    {
        gameObject = new GameObject();
        gameObject.transform.position = Position;
        gameObject.name = $"Chunk {X},{Y},{Z}";

        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Voxel;

        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = Mesh;
    }

    public static Chunk Create(BlockChunk blocks, Material material, int x, int y, int z, GameObject parent, Mesh mesh)
    {
        var chunk = new Chunk
        {
            Blocks = blocks,
            Voxel = material,
            X = x,
            Y = y,
            Z = z,
            Mesh = mesh,
            Position = x * Vector3.left * BlockChunk.SIZE + z * Vector3.forward * BlockChunk.SIZE + y * Vector3.up * BlockChunk.SIZE
        };
        chunk.Initialize();

        chunk.gameObject.transform.SetParent(parent.transform);
        return chunk;
    }
}
