using System.Collections.Generic;

public sealed class BlockChunk
{
    public const int SIZE = 8;

    private BlockType[,,] blocks;

    public int Width => blocks.GetLength(0);
    public int Length => blocks.GetLength(1);
    public int Depth => blocks.GetLength(2);
    public BlockType[,,] Blocks => blocks;

    public BlockChunk()
    {
        blocks = new BlockType[SIZE, SIZE, SIZE];
    }

    public IEnumerable<Block> ToList()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Length; y++)
                for (int z = 0; z < Depth; z++)
                    yield return new Block(x, y, z, Blocks[x, y, z]);
    }

    public static BlockChunk Default(BlockType type) {
        var chunk = new BlockChunk();
        foreach (var block in chunk.ToList())
            chunk.Blocks[block.X, block.Y, block.Z] = type;

        return chunk;
    }

    public static BlockChunk Assorted(BlockType[] types) {
        var chunk = new BlockChunk();
        foreach (var block in chunk.ToList())
        {
            var idx = (block.Y * chunk.Width + block.X) % types.Length;
            chunk.Blocks[block.X, block.Y, block.Z] = types[idx];
        }
        return chunk;
    }
}
