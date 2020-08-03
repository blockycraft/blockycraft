using UnityEngine;
using System.Collections.Generic;

namespace Assets.Engine.Block
{
    [CreateAssetMenu(fileName = "Pack", menuName = "Blockycraft/Texture Pack")]
    public sealed class TexturePack : ScriptableObject
    {
        [Header("Descriptors")]
        public string Name;
        public int Width;
        public int Height;
        public float Scale;
        public string defaultKey;

        [Header("Graphics")]
        public Material Material;

        [Header("Sprites")]
        public TextAsset Definition;

        private Dictionary<string, Element> lookup;

        public void OnValidate()
        {
            if (Definition == null) { return; }
            var elements = JsonHelper.FromJson<Element>(Definition.text);

            lookup = new Dictionary<string, Element>();
            foreach (var element in elements)
            {
                lookup[element.name] = element;
            }
        }

        public Element Find(string key)
        {
            if (lookup.ContainsKey(key)) return lookup[key];
            return lookup[defaultKey];
        }

        public Vector2 UV(Element element)
        {
            return new Vector2(
                (float)element.x / Width,
                1f - (float)element.y / Height
            );
        }

        public Vector2 Dimensions(Element element)
        {
            return new Vector2(
                (float)element.width / Width,
                -(float)element.height / Height
            );
        }

        [System.Serializable]
        public sealed class Element
        {
            public string name;
            public int x;
            public int y;
            public int width;
            public int height;
        }
    }
}