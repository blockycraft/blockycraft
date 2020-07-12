public sealed class WorldGenerator
{
    public static BlockChunk Generate(Biome biome)
    {
        var chunk = new BlockChunk();
        foreach (var block in chunk.ToList()) {
            var idx = block.Y % biome.Blocks.Count;
            chunk.Blocks[block.X, block.Y, block.Z] = biome.Blocks[idx].Type;
        }
        return chunk;
    }

    public static BlockChunk Default(BlockType type)
    {
        var chunk = new BlockChunk();
        foreach (var block in chunk.ToList())
            chunk.Blocks[block.X, block.Y, block.Z] = type;

        return chunk;
    }

    public static BlockChunk Assorted(BlockType[] types)
    {
        var chunk = new BlockChunk();
        foreach (var block in chunk.ToList())
        {
            var idx = (block.Y * chunk.Width + block.X) % types.Length;
            chunk.Blocks[block.X, block.Y, block.Z] = types[idx];
        }
        return chunk;
    }
}
