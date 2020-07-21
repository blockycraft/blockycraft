using UnityEngine;

namespace Assets.Scripts.Geometry
{
    public static class ChunkFactory
    {
        public static readonly int GridSize = 8;
        public static float GridUVFactor { get { return 1f / (float)GridSize; } }

        public static bool IsObscured(BlockType[,,] blocks, int x, int y, int z)
        {
            if (x < 0 || x >= blocks.GetLength(0) ||
                y < 0 || y >= blocks.GetLength(1) ||
                z < 0 || z >= blocks.GetLength(2))
            {
                return false;
            }

            return blocks[x, y, z] != null;
        }

        public static int ComputeVisibleFaces(BlockChunk blocks)
        {
            var visible = 0;
            var iterator = blocks.GetIterator();
            var directions = System.Enum.GetValues(typeof(BlockFace));
            foreach (var (x, y, z) in iterator)
            {
                var type = blocks.Blocks[x, y, z];
                foreach (int face in directions)
                {
                    var (nx, ny, nz) = BlockChunk.GetDirection(x, y, z, (BlockFace)face);
                    if (!IsObscured(blocks.Blocks, nx, ny, nz))
                    {
                        visible++;
                    }
                }
            }
            return visible;
        }

        public static ChunkFab CreateFromBlocks(BlockChunk blocks)
        {
            var faces = ComputeVisibleFaces(blocks);
            var meshFab = new ChunkFab(blocks, faces);
            int vertexIndex = 0;
            var blockSize = 1.0f;
            var directions = System.Enum.GetValues(typeof(BlockFace));

            var iterator = blocks.GetIterator();
            foreach (var (x, y, z) in iterator)
            {
                var type = blocks.Blocks[x, y, z];

                var offset = (x * Vector3.right * blockSize) + (z * Vector3.forward * blockSize) + (y * Vector3.up * blockSize);
                foreach (int face in directions)
                {
                    var (nx, ny, nz) = BlockChunk.GetDirection(x, y, z, (BlockFace)face);
                    if (IsObscured(blocks.Blocks, nx, ny, nz))
                    {
                        continue;
                    }

                    for (int vert = 0; vert < Voxel.VerticesInFace; vert++)
                    {
                        meshFab.PushVertex(offset + Voxel.Vertices[Voxel.Tris[face, vert]]);
                    }

                    var texture = BlockType.GetTextureID(type, face);
                    var uv = type.textures.UV(texture);
                    var dimensions = type.textures.Dimensions(texture);

                    meshFab.PushUV(new Vector2(uv.x + dimensions.x, uv.y + dimensions.y));
                    meshFab.PushUV(new Vector2(uv.x + dimensions.x, uv.y));
                    meshFab.PushUV(new Vector2(uv.x, uv.y + dimensions.y));
                    meshFab.PushUV(uv);

                    for (int idx = 0; idx < Voxel.Triangles.Length; idx++)
                        meshFab.PushTriangle(vertexIndex + Voxel.Triangles[idx]);

                    vertexIndex += Voxel.VerticesInFace;
                }
            }

            return meshFab;
        }
    }

    public sealed class ChunkFab
    {
        public BlockChunk Blocks { get; private set; }
        public Vector3[] Verticies { get; private set; }
        public int[] Triangles { get; private set; }
        public Vector2[] UVs { get; private set; }
        private int idxVertex, idxUV, idxTriangles;

        public ChunkFab(BlockChunk blocks, int faces) {
            Verticies = new Vector3[Voxel.VerticesInFace * faces];
            UVs = new Vector2[Voxel.VerticesInFace * faces];
            Triangles = new int[Voxel.Triangles.Length * faces];
            idxVertex = idxUV = idxTriangles = 0;
            Blocks = blocks;
        }

        public void PushUV(Vector2 uv)
        {
            UVs[idxUV] = uv;
            idxUV++;
        }

        public void PushVertex(Vector3 vertex)
        {
            Verticies[idxVertex] = vertex;
            idxVertex++;
        }

        public void PushTriangle(int triangle)
        {
            Triangles[idxTriangles] = triangle;
            idxTriangles++;
        }

        public Mesh ToMesh()
        {
            var mesh = new Mesh
            {
                vertices = Verticies,
                triangles = Triangles,
                uv = UVs,
            };

            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
