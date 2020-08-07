using UnityEngine;

public sealed class Object3D<TElement>
{
    public Vector3Int Coordinate { get; private set; }
    public TElement Element { get; private set; }

    public Object3D(Vector3Int coord, TElement element)
    {
        Coordinate = coord;
        Element = element;
    }

    public Object3D(int x, int y, int z, TElement element)
    {
        Coordinate = new Vector3Int(x, y, z);
        Element = element;
    }
}
