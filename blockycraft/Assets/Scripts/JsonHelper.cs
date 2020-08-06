using UnityEngine;

namespace Blockycraft.Scripts
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        [System.Serializable]
        private sealed class Wrapper<T>
        {
            public T[] Items;
        }
    }
}