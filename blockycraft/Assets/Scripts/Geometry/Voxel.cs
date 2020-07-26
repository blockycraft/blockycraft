﻿using System;
using UnityEngine;

namespace Assets.Scripts.Geometry
{
    public static class Voxel
    {
        public const int VerticesInFace = 4;
        public const int NumberOfFaces = 6;

        public static readonly Vector3 Center = new Vector3(0.5f, 0.5f, 0.5f);

        public static readonly Vector3[] Vertices = new Vector3[8]
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 1.0f),
            new Vector3(1.0f, 0.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(0.0f, 1.0f, 1.0f)
        };

        public static readonly int[] Triangles = new int[6]{
            0,1,2,2,1,3
        };

        public static readonly int[,] Tris = new int[NumberOfFaces, VerticesInFace] {
            {0, 3, 1, 2},
            {5, 6, 4, 7},
            {3, 7, 2, 6},
            {1, 5, 0, 4},
            {4, 7, 0, 3},
            {1, 2, 5, 6}
        };

        public static readonly float Scale = 1.0f;

        public static Vector3 Position(Vector3Int coord)
        {
            return new Vector3(-1 * coord.x * 1.0f * Scale, coord.y * Scale, coord.z * Scale);
        }

        public static Vector3Int Direction(VoxelFace face)
        {
            switch (face)
            {
                case VoxelFace.Back: return new Vector3Int(-1, 0, 0);
                case VoxelFace.Front: return new Vector3Int(1, 0, 0);
                case VoxelFace.Left: return new Vector3Int(0, 0, -1);
                case VoxelFace.Right: return new Vector3Int(0, 0, 1);
                case VoxelFace.Top: return new Vector3Int(0, 1, 0);
                case VoxelFace.Bottom: return new Vector3Int(0, -1, 0);
                default: throw new ArgumentException();
            }
        }
    }
}