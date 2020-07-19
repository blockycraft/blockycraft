using UnityEngine;

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

    public static Vector2 UV(BlockType block, int face, int sizeOfGrid, float uvFactor)
    {
        var textureID = (int)BlockType.GetTextureID(block, face);
        float y = textureID / sizeOfGrid;
        float x = textureID - (y * sizeOfGrid);
        return new Vector2(
            x * uvFactor,
            1f - (y + 1) * uvFactor
        );
    }

}
