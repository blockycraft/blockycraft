public sealed class WorldGenerator
{
    public static BlockChunk Generate(Biome biome, int chunkX, int chunkZ)
    {
        var chunk = new BlockChunk(chunkX, chunkZ);
        var iterator = chunk.GetIterator();
        foreach (var (x, y, z) in iterator)
        {
            var idx = y % biome.Blocks.Count;
            chunk.Blocks[x, y, z] = biome.Blocks[idx].Type;
        }
        return chunk;
    }

    public static BlockChunk Default(BlockType type, int chunkX, int chunkZ)
    {
        var chunk = new BlockChunk(chunkX, chunkZ);
        var iterator = chunk.GetIterator();
        foreach (var (x, y, z) in iterator)
            chunk.Blocks[x, y, z] = type;

        return chunk;
    }

    public static BlockChunk Assorted(BlockType[] types, int chunkX, int chunkZ)
    {
        var chunk = new BlockChunk(chunkX, chunkZ);
        var iterator = chunk.GetIterator();
        foreach (var (x, y, z) in iterator)
        {
            var idx = (y * chunk.Width + x) % types.Length;
            chunk.Blocks[x, y, z] = types[idx];
        }
        return chunk;
    }
}