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

        public static Vector2 Perlin2DSample(int x, int y, int size, float offset, float scale)
        {
            var epsilon = 0.1f;
            return new Vector2(
                ((x + epsilon) / size + offset) * scale,
                ((y + epsilon) / size + offset) * scale
            );
        }

        public static Vector3 Perlin3DSample(int x, int y, int z, float offset, float scale)
        {
            var epsilon = 0.1f;
            return new Vector3(
                (x + offset + epsilon) * scale,
                (y + offset + epsilon) * scale,
                (z + offset + epsilon) * scale
            );
        }

        public static float Get3DPerlin(float x, float y, float z)
        {
            const float points = 6f;
            float xy = Mathf.PerlinNoise(x, y);
            float yz = Mathf.PerlinNoise(y, z);
            float xz = Mathf.PerlinNoise(x, z);
            float yx = Mathf.PerlinNoise(y, x);
            float zy = Mathf.PerlinNoise(z, y);
            float zx = Mathf.PerlinNoise(z, x);

            return (xy + yz + xz + yx + zy + zx) / points;
        }
    }
}