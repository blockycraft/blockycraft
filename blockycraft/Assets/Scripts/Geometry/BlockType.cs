using UnityEngine;

[CreateAssetMenu(fileName = "BlockType", menuName = "Blockycraft/Block Type")]
public sealed class BlockType : ScriptableObject
{

    [Header("Descriptors")]
    public string blockName;

    public Material material;

    [Header("Texture Faces")]
    public BlockTypeTexture left;
    public BlockTypeTexture right;
    public BlockTypeTexture top;
    public BlockTypeTexture bottom;
    public BlockTypeTexture front;
    public BlockTypeTexture back;

    public static BlockTypeTexture GetTextureID(BlockType block, int index)
    {
        switch ((BlockFace)index)
        {
            case BlockFace.Back:
                return block.back;
            case BlockFace.Front:
                return block.front;
            case BlockFace.Top:
                return block.top;
            case BlockFace.Bottom:
                return block.bottom;
            case BlockFace.Left:
                return block.left;
            case BlockFace.Right:
                return block.right;
            default:
                return BlockTypeTexture.Dirt;
        }
    }
}
