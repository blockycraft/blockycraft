﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Blockycraft.World
{
    public sealed class Space3D<TElement> : IEnumerable<Object3D<TElement>>
    {
        public int Count { get { return elements.Count; } }
        public bool IsEmpty { get { return elements.Count == 0; } }
        private readonly Dictionary<string, TElement> elements;

        public Space3D()
        {
            elements = new Dictionary<string, TElement>();
        }

        public TElement Get(int x, int y, int z)
        {
            var key = Key(x, y, z);
            return elements[key];
        }

        public TElement Get(Vector3Int coordinate)
        {
            var key = Key(coordinate.x, coordinate.y, coordinate.z);
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
                elements[key] = selector(adjusted);
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
                var dims = item.Key.Split(':');
                yield return new Object3D<TElement>(int.Parse(dims[0]), int.Parse(dims[1]), int.Parse(dims[2]), item.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}