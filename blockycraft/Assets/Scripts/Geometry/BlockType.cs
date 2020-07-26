using UnityEngine;
using System;
using Assets.Scripts;

namespace Assets.Scripts.Geometry
{
    [CreateAssetMenu(fileName = "BlockType", menuName = "Blockycraft/Block Type")]
    public sealed class BlockType : ScriptableObject
    {

        [Header("Descriptors")]
        public string blockName;

        public Material material;
        public TexturePack textures;

        [Header("Properties")]
        public bool isVisible;

        [Header("Texture Faces")]
        public string left;
        public string right;
        public string top;
        public string bottom;
        public string front;
        public string back;

        public BlockType()
        {
            isVisible = true;
        }

        public static TexturePack.Element GetTextureID(BlockType block, int index)
        {
            switch ((BlockFace)index)
            {
                case BlockFace.Back:
                    return block.textures.Find(block.back);
                case BlockFace.Front:
                    return block.textures.Find(block.front);
                case BlockFace.Top:
                    return block.textures.Find(block.top);
                case BlockFace.Bottom:
                    return block.textures.Find(block.bottom);
                case BlockFace.Left:
                    return block.textures.Find(block.left);
                case BlockFace.Right:
                    return block.textures.Find(block.right);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}