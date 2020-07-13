public sealed class WorldGenerator
{
    public static BlockChunk Generate(Biome biome)
    {
        var chunk = new BlockChunk();
        var iterator = chunk.GetIterator();
        foreach (var block in iterator) {
            var idx = (int)(block.y % biome.Blocks.Count);
            chunk.Blocks[(int)block.x, (int)block.y, (int)block.z] = biome.Blocks[idx].Type;
        }
        return chunk;
    }

    public static BlockChunk Default(BlockType type)
    {
        var chunk = new BlockChunk();
        var iterator = chunk.GetIterator();
        foreach (var block in iterator)
            chunk.Blocks[(int)block.x, (int)block.y, (int)block.z] = type;

        return chunk;
    }

    public static BlockChunk Assorted(BlockType[] types)
    {
        var chunk = new BlockChunk();
        var iterator = chunk.GetIterator();
        foreach (var block in iterator)
        {
            var idx = (int) ((block.y * chunk.Width + block.x) % types.Length);
            chunk.Blocks[(int)block.x, (int)block.y, (int)block.z] = types[idx];
        }
        return chunk;
    }
}
