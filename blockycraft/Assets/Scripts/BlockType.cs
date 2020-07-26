using Assets.Scripts.Geometry;
using System;
using UnityEngine;

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
            return ((VoxelFace)index) switch
            {
                VoxelFace.Back => block.textures.Find(block.back),
                VoxelFace.Front => block.textures.Find(block.front),
                VoxelFace.Top => block.textures.Find(block.top),
                VoxelFace.Bottom => block.textures.Find(block.bottom),
                VoxelFace.Left => block.textures.Find(block.left),
                VoxelFace.Right => block.textures.Find(block.right),
                _ => throw new NotSupportedException(),
            };
        }
    }
}