using Blockycraft.Engine.Geometry;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blockycraft.World.Chunk
{
    public sealed class ChunkFactory
    {
        private sealed class WorkItem
        {
            public ChunkBlocks Blocks;
            public ChunkFab Fab;
        }

        private readonly Queue<WorkItem> processed;
        private readonly Space3D<WorkItem> queue;

        public ChunkFactory()
        {
            queue = new Space3D<WorkItem>();
            processed = new Queue<WorkItem>();
        }

        public void Enqueue(ChunkBlocks blocks)
        {
            if (queue.Contains(blocks.X, blocks.Y, blocks.Z)) { return; }

            var work = new WorkItem()
            {
                Blocks = blocks
            };
            queue.Set(blocks.X, blocks.Y, blocks.Z, work);
        }

        public bool Process(Vector3Int position)
        {
            if (queue.IsEmpty)
            {
                return false;
            }

            var work = queue.Closest(position);
            queue.Remove(work.Blocks.X, work.Blocks.Y, work.Blocks.Z);

            var visibility = Visibility(work.Blocks);
            var initFab = new ChunkFab(visibility.Count);
            work.Fab = CreateFromBlocks(work.Blocks, visibility, initFab);

            processed.Enqueue(work);
            return true;
        }

        public Space3D<Mesh> Completed()
        {
            var system = new Space3D<Mesh>();
            foreach (var work in processed)
            {
                var mesh = Compile(work.Fab);
                system.Set(work.Blocks.Coordinate, mesh);
            }
            processed.Clear();
            return system;
        }

        private static ChunkView Visibility(ChunkBlocks blocks)
        {
            var iterator = blocks.GetIterator();
            var directions = System.Enum.GetValues(typeof(VoxelFace));
            var visibility = new ChunkView(blocks.Length, blocks.Height, blocks.Depth);
            foreach (var coord in iterator)
            {
                var type = blocks.Blocks[coord.x, coord.y, coord.z];
                if (!type.isVisible)
                {
                    visibility.Blocks[coord.x, coord.y, coord.z] = true;
                    continue;
                }
                visibility.Blocks[coord.x, coord.y, coord.z] = true;

                foreach (int face in directions)
                {
                    var neighbour = coord + Voxel.Direction((VoxelFace)face);
                    if (blocks.TryGet(ref neighbour, out BlockType neighbourType) && neighbourType.IsObscure())
                    {
                        visibility.Faces[coord.x, coord.y, coord.z, face] = false;
                    }
                    else
                    {
                        visibility.Increment();
                        visibility.Faces[coord.x, coord.y, coord.z, face] = true;
                    }
                }
            }
            return visibility;
        }

        private static ChunkFab CreateFromBlocks(ChunkBlocks blocks, ChunkView view, ChunkFab meshFab)
        {
            int vertexIndex = 0;
            var directions = Enum.GetValues(typeof(VoxelFace));

            var iterator = blocks.GetIterator();
            foreach (var coord in iterator)
            {
                if (!view.Blocks[coord.x, coord.y, coord.z]) { continue; }

                var type = blocks.Blocks[coord.x, coord.y, coord.z];
                var offset = Voxel.Position(coord);
                foreach (int face in directions)
                {
                    var neighbour = coord + Voxel.Direction((VoxelFace)face);
                    if (!view.Faces[coord.x, coord.y, coord.z, face]) { continue; }

                    for (int vert = 0; vert < Voxel.VerticesInFace; vert++)
                    {
                        meshFab.PushVertex(offset + Voxel.Vertices[Voxel.Tris[face, vert]]);
                    }

                    var textureID = BlockType.GetTextureID(type, (VoxelFace)face);
                    var texture = type.textures.Find(textureID);
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

        private static Mesh Compile(ChunkFab fab)
        {
            var mesh = new Mesh
            {
                vertices = fab.Verticies,
                triangles = fab.Triangles,
                uv = fab.UVs,
            };

            mesh.RecalculateNormals();
            return mesh;
        }

        public static Mesh Build(BlockType type)
        {
            var blockChunk = new ChunkBlocks(0, 0, 0, 1);
            blockChunk.Blocks[0, 0, 0] = type;
            return Build(blockChunk);
        }

        public static Mesh Build(ChunkBlocks blockChunk)
        {
            var visibility = Visibility(blockChunk);
            var initFab = new ChunkFab(visibility.Count);
            var chunkFab = CreateFromBlocks(blockChunk, visibility, initFab);

            var mesh = Compile(chunkFab);
            return mesh;
        }
    }
}