using Blockycraft;
using UnityEngine;
using UnityEngine.UI;

public sealed class BlockSelector : MonoBehaviour
{
    public BlockType[] Types;
    public int Index;
    public RawImage Image;

    public void Update()
    {
        Image.texture = Types[Index].preview;
    }

}
