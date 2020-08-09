# Unity Development

Below I have included a colletion of notes relating to the development of Unity. These are intended to give brief overviews of the features, included with some thoughts.

## Unity Inspector

A BlockType defines any block that can exist within the block engine. This defines the texture configuration and visibility properties of all blocks in the world. Rather than rely on just a data file (JSON/YAML), I thought it would be a good idea to make use of the Unity Inspector's custom editor options.

The BlockType has a custom editor that shows a preview of each block, and supports rotation to view each face. You can see an example of this below:

<>

A pain point I noted is with the string lookup for each block face. Rather than be a lookup window (showing each of the textures), it requires knowing the name of the texture from a map.

### Additional Note

When a biome is defined, block types must be passed in that will be used by the generator. Each of these types has a preview associated with it when you are looking it up in the Unity Editor. This is an extremely nice quality of life feature. I have included a screenshot below of this quality of life improvement:

<>

## Unity Threading

I really intend for this project to be deployed by WebGL, which limits the threading options for the project. To workaround this the ChunkFactory and related methods were devised. Splitting it up this way increases the memory burden, but exposes the option long-term for splitting each action (generation, visibility, allocation, mesh generation) into its own unit of work.

These units can then be processed on a per-frame basis to reduce the load on the main thread. This does mean that things don't load in very quickly, but it doesn't have the same jittering that would be encountered if `N` chunks were generated in a single frame.

## GitHub Actions

Deployments of the original blockycraft were not really possible. With GitHub Actions this process was extremely efficient. Although there were some quirks, but nothing that would add significant technical debt to the project.

Positive.