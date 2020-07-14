using System.Collections.Generic;
using UnityEngine;

public sealed class VoxelBuilder
{
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

    public Mesh Build(BlockType block, Vector3 position)
    {
        int vertexIndex = 0;
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();

        for (int face = 0; face < Voxel.NumberOfFaces; face++)
        {
            for (int vert = 0; vert < Voxel.VerticesInFace; vert++)
                vertices.Add(position + Voxel.Vertices[Voxel.Tris[face, vert]]);

            var uv = ComputeUV(block, face);
            uvs.Add(uv);
            uvs.Add(new Vector2(uv.x, uv.y + GridUVFactor));
            uvs.Add(new Vector2(uv.x + GridUVFactor, uv.y));
            uvs.Add(new Vector2(uv.x + GridUVFactor, uv.y + GridUVFactor));

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

    public Mesh Build(BlockChunk blocks, Vector3 position)
    {
        int vertexIndex = 0;
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var uvs = new List<Vector2>();
        var iterator = blocks.GetIterator();

        foreach (var (x, y, z) in iterator)
        {
            var type = blocks.Blocks[x, y, z];

            var offset = x * Vector3.right + z * Vector3.forward + y * Vector3.up;
            for (int face = 0; face < Voxel.NumberOfFaces; face++)
            {
                for (int vert = 0; vert < Voxel.VerticesInFace; vert++)
                    vertices.Add(position + offset + Voxel.Vertices[Voxel.Tris[face, vert]]);

                var uv = ComputeUV(type, face);
                uvs.Add(uv);
                uvs.Add(new Vector2(uv.x, uv.y + GridUVFactor));
                uvs.Add(new Vector2(uv.x + GridUVFactor, uv.y));
                uvs.Add(new Vector2(uv.x + GridUVFactor, uv.y + GridUVFactor));

                for (int idx = 0; idx < Voxel.Triangles.Length; idx++)
                    triangles.Add(vertexIndex + Voxel.Triangles[idx]);
                
                vertexIndex += Voxel.VerticesInFace;
            }
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
