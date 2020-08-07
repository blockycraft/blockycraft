using Blockycraft;
using Blockycraft.Engine.Geometry;
using Blockycraft.World;
using Blockycraft.World.Chunk;
using UnityEngine;

public sealed class Chunk
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public Mesh Mesh { get; set; }
    public ChunkBlocks Blocks { get; set; }
    public Material MeshMaterial { get; set; }
    public GameObject gameObject { get; set; }
    public Vector3 Position { get; set; }

    private Chunk()
    { }

    private void Initialize()
    {
        gameObject = new GameObject();
        gameObject.transform.position = Position;
        gameObject.name = $"Chunk {Blocks.X},{Blocks.Y},{Blocks.Z}";

        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = MeshMaterial;

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
            MeshMaterial = material,
            Mesh = mesh,
            Position = Voxel.Position(x, y, z) * World.SIZE
        };
        chunk.Initialize();

        chunk.gameObject.transform.SetParent(parent.transform);
        return chunk;
    }
}