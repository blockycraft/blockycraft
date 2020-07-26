using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Iterator3D : IEnumerable<Vector3Int>
{
    public int Width { get; }
    public int Height { get; }
    public int Depth { get; }

    public Iterator3D(int size) : this(size, size, size)
    {
    }

    public Iterator3D(int width, int height, int depth)
    {
        Width = width;
        Height = height;
        Depth = depth;
    }

    public IEnumerator<Vector3Int> GetEnumerator()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                for (int z = 0; z < Depth; z++)
                    yield return new Vector3Int(x, y, z);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}