using Assets.Scripts.Geometry;
using UnityEngine;

namespace Assets.Scripts.World.Chunk
{
    public sealed class ChunkFab
    {
        public Vector3[] Verticies { get; }
        public Vector2[] UVs { get; }
        public int[] Triangles { get; }
        private int idxVertex, idxUV, idxTriangles;

        public ChunkFab(int faces)
        {
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
    }
}