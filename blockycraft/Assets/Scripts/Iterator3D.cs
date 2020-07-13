using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class Iterator3D : IEnumerable<Vector3>
{
    public int Width { get; }
    public int Length { get; }
    public int Depth { get; }

    public Iterator3D(int size) : this(size, size, size) { }

    public Iterator3D(int width, int length, int depth)
    {
        Width = width;
        Length = length;
        Depth = depth;
    }

    public IEnumerator<Vector3> GetEnumerator()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Length; y++)
                for (int z = 0; z < Depth; z++)
                    yield return new Vector3(x, y, z);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
