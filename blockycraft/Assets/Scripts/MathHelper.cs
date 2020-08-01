using UnityEngine;

namespace Assets.Scripts
{
    public static class MathHelper
    {
        public static int Wrap(int value, int size)
        {
            var num = value % size;
            return (value >= 0) ? num : (num == 0) ? num : num + size;
        }

        public static Vector3Int Wrap(int x, int y, int z, int size)
        {
            return new Vector3Int(
                Wrap(x, size),
                Wrap(y, size),
                Wrap(z, size)
            );
        }

        public static int Anchor(int value, int size)
        {
            return (value >= 0) 
                ? Mathf.FloorToInt(value / size) 
                : -(Mathf.FloorToInt((Mathf.Abs(value) - 1) / size) + 1);
        }

        public static Vector3Int Anchor(int x, int y, int z, int size)
        {
            return new Vector3Int(
                Anchor(x, size),
                Anchor(y, size),
                Anchor(z, size)
            );
        }
    }
}
