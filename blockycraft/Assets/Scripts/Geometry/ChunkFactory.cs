using UnityEngine;

namespace Assets.Scripts.Geometry
{
    public static class ChunkFactory
    {
        public static bool IsVisible(BlockType[,,] blocks, int x, int y, int z)
        {
            if (x < 0 || x >= blocks.GetLength(0) ||
                y < 0 || y >= blocks.GetLength(1) ||
                z < 0 || z >= blocks.GetLength(2))
            {
                return false;
            }

            return blocks[x, y, z].isVisible;
        }

        public static int ComputeVisibleFaces(BlockChunk blocks)
        {
            var visible = 0;
            var iterator = blocks.GetIterator();
            var directions = System.Enum.GetValues(typeof(BlockFace));
            foreach (var coord in iterator)
            {
                var type = blocks.Blocks[coord.x, coord.y, coord.z];
                if (!type.isVisible) continue;

                foreach (int face in directions)
                {
                    var neighbour = BlockChunk.GetDirection(coord.x, coord.y, coord.z, (BlockFace)face);
                    if (IsVisible(blocks.Blocks, neighbour.x, neighbour.y, neighbour.z))
                    {
                        continue;
                    }

                    visible++;
                }
            }
            return visible;
        }

        public static ChunkFab CreateFromBlocks(BlockChunk blocks)
        {
            var faces = ComputeVisibleFaces(blocks);
            var meshFab = new ChunkFab(faces);
            int vertexIndex = 0;
            var blockSize = 1.0f;
            var directions = System.Enum.GetValues(typeof(BlockFace));

            var iterator = blocks.GetIterator();
            foreach (var coord in iterator)
            {
                var type = blocks.Blocks[coord.x, coord.y, coord.z];
                if (!type.isVisible) continue;

                var offset = (coord.x * Vector3.left * blockSize) + (coord.z * Vector3.forward * blockSize) + (coord.y * Vector3.up * blockSize);
                foreach (int face in directions)
                {
                    var neighbour = BlockChunk.GetDirection(coord.x, coord.y, coord.z, (BlockFace)face);
                    if (IsVisible(blocks.Blocks, neighbour.x, neighbour.y, neighbour.z))
                    {
                        continue;
                    }

                    for (int vert = 0; vert < Voxel.VerticesInFace; vert++)
                    {
                        meshFab.PushVertex(offset + Voxel.Vertices[Voxel.Tris[face, vert]]);
                    }

                    var texture = BlockType.GetTextureID(type, face);
                    var uv = type.textures.UV(texture);
                    var dimensions = type.textures.Dimensions(texture);

                    meshFab.PushUV(new Vector2(uv.x + dimensions.x, uv.y + dimensions.y));
                    meshFab.PushUV(new Vector2(uv.x + dimensions.x, uv.y));
                    meshFab.PushUV(new Vector2(uv.x, uv.y + dimensions.y));
                    meshFab.PushUV(uv);

                    for (int idx = 0; idx < Voxel.Triangles.Length; idx++)
                        meshFab.PushTriangle(vertexIndex + Voxel.Triangles[idx]);

                    vertexIndex += Voxel.VerticesInFace;
                }
            }

            return meshFab;
        }
    }
}
