using Blockycraft;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class BlockSelector : MonoBehaviour
{
    public RawImage Image;
    public string DefaultKey;
    private BlockType[] types;
    private int index;

    public BlockType Selected
    {
        get { return types[index]; }
    }

    public void Start()
    {
        types = Resources.LoadAll<BlockType>("BlockTypes").Where(p => p.isVisible).ToArray();
        index = Array.FindIndex(types, p => p.blockName.Equals(DefaultKey));
        if (index < 0) { index = types.Length - 1; }
        else if (index >= types.Length) { index = 0; }

        Image.texture = types[index].preview;
    }

    public void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
            {
                index++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
            {
                index--;
            }

            if (index < 0) { index = types.Length - 1; }
            else if (index >= types.Length) { index = 0; }

            Image.texture = types[index].preview;
        }
    }

}
