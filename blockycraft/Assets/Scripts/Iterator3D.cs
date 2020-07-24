using System.Collections;
using System.Collections.Generic;

public sealed class Iterator3D : IEnumerable<(int x, int y, int z)>
{
    public int Width { get; }
    public int Height { get; }
    public int Depth { get; }

    public Iterator3D(int size) : this(size, size, size) { }

    public Iterator3D(int width, int length, int depth)
    {
        Width = width;
        Height = length;
        Depth = depth;
    }

    public IEnumerator<(int x, int y, int z)> GetEnumerator()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                for (int z = 0; z < Depth; z++)
                    yield return (x, y, z);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}