using UnityEngine;

namespace Assets.Scripts.Geometry
{
    public sealed class ChunkFab
    {
        // is this necessary?
        public Vector3[] Verticies { get; private set; }
        public int[] Triangles { get; private set; }
        public Vector2[] UVs { get; private set; }
        private int idxVertex, idxUV, idxTriangles;

        public ChunkFab(int faces) {
            Verticies = new Vector3[Voxel.VerticesInFace * faces];
            UVs = new Vector2[Voxel.VerticesInFace * faces];
            Triangles = new int[Voxel.Triangles.Length * faces];
            idxVertex = idxUV = idxTriangles = 0;
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
