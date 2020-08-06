using System;
using UnityEngine;
using Assets.Engine.Geometry;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "BlockType", menuName = "Blockycraft/Block Type")]
    public sealed class BlockType : ScriptableObject
    {
        [Header("Descriptors")]
        public string blockName;

        public TexturePack textures;

        [Header("Properties")]
        [Tooltip("Determines if the block has a visibility component.")]
        public bool isVisible;

        [Tooltip("Determines if the block can be seen through.")]
        public bool isTransparent;

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
            isTransparent = false;
        }

        public bool IsObscure()
        {
            return isVisible && !isTransparent;
        }

        public bool IsValid()
        {
            if (textures == null)
            {
                return false;
            }
            if (
                string.IsNullOrEmpty(back) ||
                string.IsNullOrEmpty(front) ||
                string.IsNullOrEmpty(top) ||
                string.IsNullOrEmpty(bottom) ||
                string.IsNullOrEmpty(left) ||
                string.IsNullOrEmpty(right)
               )
            {
                return false;
            }

            return true;
        }

        public static string GetTextureID(BlockType block, VoxelFace face)
        {
            switch (face)
            {
                case VoxelFace.Back: return block.back;
                case VoxelFace.Front: return block.front;
                case VoxelFace.Top: return block.top;
                case VoxelFace.Bottom: return block.bottom;
                case VoxelFace.Left: return block.left;
                case VoxelFace.Right: return block.right;
                default: throw new NotSupportedException();
            };
        }
    }
}