using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blockycraft.World
{
    public sealed class System3D<TElement> : IEnumerable<System3D<TElement>.Element>
    {
        public sealed class Element
        {
            public Vector3Int Coordinate;
            public TElement Value;
        }

        private readonly Dictionary<string, TElement> elements;

        public int Count { get { return elements.Count; } }
        public bool IsEmpty { get { return elements.Count == 0; } }

        public System3D()
        {
            elements = new Dictionary<string, TElement>();
        }

        public TElement Get(int x, int y, int z)
        {
            var key = Key(x, y, z);
            return elements[key];
        }

        public void Set(int x, int y, int z, TElement element)
        {
            var key = Key(x, y, z);
            elements[key] = element;
        }

        public void Set(Vector3Int position, TElement element)
        {
            var key = Key(position.x, position.y, position.z);
            elements[key] = element;
        }

        public bool TryGet(ref Vector3Int position, out TElement type)
        {
            var key = Key(position.x, position.y, position.z);
            if (!elements.ContainsKey(key))
            {
                type = default;
                return false;
            }

            type = elements[key];
            return true;
        }

        public IEnumerable<Vector3Int> Radial(Vector3Int position, int radius)
        {
            var iterator = new Iterator3D(radius * 2);
            foreach (var coord in iterator)
            {
                var x = position.x + (coord.x - radius);
                var y = position.y + (coord.y - radius);
                var z = position.z + (coord.z - radius);

                var key = Key(x, y, z);
                if (elements.ContainsKey(key))
                {
                    continue;
                }

                var adjusted = new Vector3Int(x, y, z);
                yield return adjusted;
            }
        }
        public void Ping(Vector3Int position, int radius, Func<Vector3Int, TElement> selector)
        {
            var iterator = new Iterator3D(radius * 2);
            foreach (var coord in iterator)
            {
                var x = position.x + (coord.x - radius);
                var y = position.y + (coord.y - radius);
                var z = position.z + (coord.z - radius);

                var key = Key(x, y, z);
                if (elements.ContainsKey(key))
                {
                    continue;
                }

                var adjusted = new Vector3Int(x, y, z);
                elements[key] = selector(adjusted);
            }
        }

        private static string Key(int x, int y, int z)
        {
            return $"{x}:{y}:{z}";
        }

        public IEnumerator<Element> GetEnumerator()
        {
            foreach (var item in elements)
            {
                yield return new Element()
                {
                    Value = item.Value,
                    Coordinate = Vector3Int.zero
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}