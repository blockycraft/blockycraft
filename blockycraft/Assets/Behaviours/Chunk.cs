using UnityEngine;

public sealed class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    public BlockChunk Blocks { get; set; }
    public Material Voxel { get; set; }
    public int X { get; set; }
    public int Z { get; set; }
    private GameObject _gObj;
    public Vector3 Position { get; set; }

    void Start()
    {
        _gObj = new GameObject();
        _gObj.transform.position = Position;
        _gObj.name = $"Chunk {X},{Z}";

        var builder = new VoxelBuilder();
        var mesh = builder.Build(Blocks, Vector3.zero);

        meshRenderer = _gObj.AddComponent<MeshRenderer>();
        meshRenderer.material = Voxel;

        meshFilter = _gObj.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
}
