using System.Collections.Generic;
using UnityEngine;

public sealed class VoxelBuilder
{
    //TODO: Use a reference to the material itself (or some custom object)
    public static readonly int GridSize = 8;
    public static float GridUVFactor { get { return 1f / (float)GridSize; }}

    public static Vector2 ComputeUV(BlockType block, int face)
    {
        var textureID = (int)BlockType.GetTextureID(block, face);
        float y = textureID / GridSize;
        float x = textureID - (y * GridSize);
        return new Vector2(
            x * GridUVFactor, 
            1f - (y + 1) * GridUVFactor
        );
    }
    public Mesh Build(IList<BlockType> blocks, Vector3 position)
    {
        int vertexIndex = 0;
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();
        var offset = Vector3.zero;

        foreach (var block in blocks)
        {
            for (int face = 0; face < Voxel.NumberOfFaces; face++)
            {
                for (int vert = 0; vert < Voxel.VerticesInFace; vert++)
                    vertices.Add(position + offset + Voxel.Vertices[Voxel.Tris[face, vert]]);

                var uv = ComputeUV(block, face);
                uvs.Add(uv);
                uvs.Add(new Vector2(uv.x, uv.y + VoxelBuilder.NormalizedBlockTextureSize));
                uvs.Add(new Vector2(uv.x + VoxelBuilder.NormalizedBlockTextureSize, uv.y));
                uvs.Add(new Vector2(uv.x + VoxelBuilder.NormalizedBlockTextureSize, uv.y + VoxelBuilder.NormalizedBlockTextureSize));

                for (int idx = 0; idx < Voxel.Triangles.Length; idx++)
                    triangles.Add(vertexIndex + Voxel.Triangles[idx]);
                
                vertexIndex += Voxel.VerticesInFace;
            }

            // TODO: Determine position by chunk location
            offset += Vector3.left;
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
