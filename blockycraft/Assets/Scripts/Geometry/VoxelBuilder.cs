using System.Collections.Generic;
using UnityEngine;

public sealed class VoxelBuilder
{
    public static readonly int GridSize = 8;
    public static float GridUVFactor { get { return 1f / (float)GridSize; }}

    public Mesh Build(BlockType block, Vector3 position)
    {
        int vertexIndex = 0;
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();

        foreach(int face in System.Enum.GetValues(typeof(BlockFace)))
        {
            for (int vert = 0; vert < Voxel.VerticesInFace; vert++)
                vertices.Add(position + Voxel.Vertices[Voxel.Tris[face, vert]]);

            var texture = BlockType.GetTextureID(block, face);
            var uv = block.textures.UV(texture);
            var dimensions = block.textures.Dimensions(texture);

            uvs.Add(new Vector2(uv.x + dimensions.x, uv.y + dimensions.y));
            uvs.Add(new Vector2(uv.x + dimensions.x, uv.y));
            uvs.Add(new Vector2(uv.x, uv.y + dimensions.y));
            uvs.Add(uv);

            for (int idx = 0; idx < Voxel.Triangles.Length; idx++)
                triangles.Add(vertexIndex + Voxel.Triangles[idx]);
            
            vertexIndex += Voxel.VerticesInFace;
        }
        
        Mesh mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            uv = uvs.ToArray()
        };

        mesh.RecalculateNormals();
        return mesh;
    }
}
