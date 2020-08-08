using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blockycraft.World
{
    public sealed class Space3D<TElement> : IEnumerable<Object3D<TElement>>
    {
        public int Count { get { return elements.Count; } }
        public bool IsEmpty { get { return elements.Count == 0; } }
        private readonly Dictionary<string, Object3D<TElement>> elements;

        public Space3D()
        {
            elements = new Dictionary<string, Object3D<TElement>>();
        }

        public TElement Get(int x, int y, int z)
        {
            var key = Key(x, y, z);
            return elements[key].Element;
        }

        public TElement Get(Vector3Int coordinate)
        {
            var key = Key(coordinate.x, coordinate.y, coordinate.z);
            return elements[key].Element;
        }

        public bool Contains(Vector3Int position)
        {
            return Contains(position.x, position.y, position.z);
        }

        public bool Contains(int x, int y, int z)
        {
            var key = Key(x, y, z);
            return elements.ContainsKey(key);
        }

        public bool Remove(Vector3Int position)
        {
            return Remove(position.x, position.y, position.z);
        }

        public bool Remove(int x, int y, int z)
        {
            var key = Key(x, y, z);
            return elements.Remove(key);
        }

        public void Set(int x, int y, int z, TElement element)
        {
            var key = Key(x, y, z);
            elements[key] = new Object3D<TElement>(x, y, z, element);
        }

        public void Set(Vector3Int position, TElement element)
        {
            var key = Key(position.x, position.y, position.z);
            elements[key] = new Object3D<TElement>(position.x, position.y, position.z, element);
        }

        public TElement Closest(Vector3Int position)
        {
            if (elements.Count == 0)
            {
                return default;
            }

            var key = Key(position.x, position.y, position.z);
            if (elements.ContainsKey(key))
            {
                return elements[key].Element;
            }

            // Algorithm: Find nearest point to position
            float nearest = float.MaxValue;
            TElement result = default;
            foreach (var element in elements)
            {
                var distance = Vector3Int.Distance(element.Value.Coordinate, position);
                if (nearest > distance)
                {
                    nearest = distance;
                    result = element.Value.Element;
                }
            }

            return result;
        }

        public bool TryGet(ref Vector3Int position, out TElement type)
        {
            var key = Key(position.x, position.y, position.z);
            if (!elements.ContainsKey(key))
            {
                type = default;
                return false;
            }

            type = elements[key].Element;
            return true;
        }

        public void Ping(Vector3Int position, Vector3Int radius, Func<Vector3Int, TElement> selector)
        {
            var iterator = new Iterator3D(radius.x, radius.y, radius.z);
            foreach (var coord in iterator)
            {
                var x = position.x + (coord.x - radius.x / 2);
                var y = position.y + (coord.y - radius.y / 2);
                var z = position.z + (coord.z - radius.z / 2);

                var key = Key(x, y, z);
                if (elements.ContainsKey(key))
                {
                    continue;
                }

                var adjusted = new Vector3Int(x, y, z);
                elements[key] = new Object3D<TElement>(adjusted, selector(adjusted));
            }
        }

        private static string Key(int x, int y, int z)
        {
            return $"{x}:{y}:{z}";
        }

        public IEnumerator<Object3D<TElement>> GetEnumerator()
        {
            foreach (var item in elements)
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}