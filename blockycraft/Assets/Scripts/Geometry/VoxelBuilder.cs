using System;
using System.Collections.Generic;
using UnityEditor;
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

    private IEnumerable<(int, int)> ListBlocks(BlockType[,] blocks) {
        for (int x = 0; x < blocks.GetLength(0); x++)
            for (int y = 0; y < blocks.GetLength(1); y++)
                yield return (x, y);
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

        foreach (var block in iterator)
        {
            var type = blocks.Blocks[(int)block.x, (int)block.y, (int)block.z];

            var offset = block.x * Vector3.right + block.z * Vector3.forward + block.y * Vector3.up;
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
