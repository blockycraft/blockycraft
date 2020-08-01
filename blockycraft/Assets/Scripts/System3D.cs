using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World
{
    public sealed class System3D<TElement>
    {
        private readonly Dictionary<string, TElement> elements;

        public System3D()
        {
            elements = new Dictionary<string, TElement>();
        }

        public TElement Get(int x, int y, int z)
        {
            var key = Key(x, y, z);
            return elements[key];
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
    }
}