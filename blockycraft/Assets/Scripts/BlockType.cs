using UnityEngine;
using System;
using Assets.Scripts.Geometry;

namespace Assets.Scripts
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
            switch ((VoxelFace)index)
            {
                case VoxelFace.Back: return block.textures.Find(block.back);
                case VoxelFace.Front: return block.textures.Find(block.front);
                case VoxelFace.Top: return block.textures.Find(block.top);
                case VoxelFace.Bottom: return block.textures.Find(block.bottom);
                case VoxelFace.Left: return block.textures.Find(block.left);
                case VoxelFace.Right: return block.textures.Find(block.right);
                default: throw new NotSupportedException();
            };
        }
    }
}