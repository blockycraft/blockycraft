using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Pack", menuName = "Blockycraft/Texture Pack")]
    public sealed class TexturePack : ScriptableObject
    {
        public Material Material;
        public string Name;
        public int Width;
        public int Height;
        public float Scale;
        public Element[] Elements;
        private System.Collections.Generic.Dictionary<string, Element> lookup;
        public string defaultKey;

        public Element Find(string key)
        {
            if (lookup == null)
            {
                lookup = new System.Collections.Generic.Dictionary<string, Element>();
                foreach (var element in Elements)
                {
                    lookup[element.Name] = element;
                }
            }
            if (lookup.ContainsKey(key)) return lookup[key];
            return lookup[defaultKey];
        }

        public Vector2 UV(Element element)
        {
            return new Vector2(
                (float)element.X / Width,
                1f - (float)element.Y / Height
            );
        }

        public Vector2 Dimensions(Element element)
        {
            return new Vector2(
                (float)element.Width / Width,
                -(float)element.Height / Height
            );
        }

        [System.Serializable]
        public sealed class Element
        {
            public string Name;
            public int X;
            public int Y;
            public int Width;
            public int Height;
        }
    }
}