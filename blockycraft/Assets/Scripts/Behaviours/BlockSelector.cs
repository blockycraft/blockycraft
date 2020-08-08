using Blockycraft;
using UnityEngine;
using UnityEngine.UI;

public sealed class BlockSelector : MonoBehaviour
{
    public BlockType[] Types;
    public int Index;
    public RawImage Image;

    public BlockType Selected
    {
        get { return Types[Index]; }
    }

    public void Start()
    {
        Image.texture = Types[Index].preview;
    }

    public void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) // forward
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            {
                Index++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            {
                Index--;
            }

            if (Index < 0) { Index = Types.Length - 1; }
            else if (Index >= Types.Length) { Index = 0; }

            Image.texture = Types[Index].preview;
        }
    }

}
