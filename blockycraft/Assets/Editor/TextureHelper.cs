using System.IO;
using UnityEngine;

namespace Blockycraft.Assets.Editor
{
    public static class TextureHelper
    {
        public static Texture2D ToTexture2D(RenderTexture renderTexture)
        {
            var texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            return texture;
        }

        public static void SaveTexture(string name, RenderTexture texture)
        {
            var filePath = Path.Combine(Application.dataPath, "Resources", "Previews", $"{name}.png");
            var text2d = ToTexture2D(texture);
            File.WriteAllBytes(filePath, text2d.EncodeToPNG());
        }
    }
}
