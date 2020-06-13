using UnityEngine;

public sealed class Chunk : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;
    private BlockChunk chunk;
    private GameObject _object;

    public GameObject GameObject { get => _object; }

    public Chunk(GameObject gobj, BlockChunk chunk) {
        this.chunk = chunk;
        _object = gobj;
    }

    void Start()
    {
        var blockTypes = ReadBlockTypes();

        builder = new VoxelBuilder();
        chunk = BlockChunk.Assorted(blockTypes);
        var mesh = builder.Build(chunk, Vector3.zero);
        meshFilter.mesh = mesh;
    }
}
