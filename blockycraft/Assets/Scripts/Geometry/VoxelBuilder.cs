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

        foreach(int face in System.Enum.GetValues(typeof(BlockFace)))
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

    public (int x, int y, int z) GetNeighbour(int x, int y, int z, BlockFace face) {
        switch(face) {
            case BlockFace.Back: return (x-1, y, z);
            case BlockFace.Front: return (x+1, y, z);
            case BlockFace.Left: return (x, y, z+1);
            case BlockFace.Right: return (x, y, z-1);
            case BlockFace.Top: return (x, y+1, z);
            case BlockFace.Bottom: return (x, y-1, z);
            default: return (x, y, z);
        }
    }

    public bool IsObscured(BlockType[,,] blocks, int x, int y, int z) {
        if (x < 0 || x >= blocks.GetLength(0) ||
            y < 0 || y >= blocks.GetLength(1) ||
            z < 0 || z >= blocks.GetLength(2)) {
            return false;
        }

        return blocks[x, y, z] != null;
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
            foreach(int face in System.Enum.GetValues(typeof(BlockFace)))
            {
                var (nx, ny, nz) = GetNeighbour(x, y, z, (BlockFace)face);
                if (IsObscured(blocks.Blocks, nx, ny, nz)) {
                    continue;
                }

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
