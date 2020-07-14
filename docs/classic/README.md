# Classic

Blockycraft is a interactive graphics demo to create a Minecraft inspired demo which revolves around breaking and placing blocks. The game world is composed of rough cubes arranged in a fixed grid pattern and representing different materials, such as dirt, stone, and snow.  The techniques used in the demo can be toggled using keyboard commands.  The Blockycraft project is written using C++ and OpenGL.

The source of the project is available in `blockycraft-classic`.

![blockycraft world](./screenshots/world.png "Blockycraft")

## Origin

The project was developed for a University of Waterloo Graphics course in Summer 2016. The intent was to learn about how various graphics techniques were created at the hardware level. The focus of this project was:

- Transparency
- Perlin Noise for World Generation
- Simple Particle System

Due to the limited timespan, the project was developed in a 'hack & slash' style. The result of this was that many of the early design decisions began to create visual or gameplay bugs that were difficult to diagnose. After submitting the project, it went through a round of refactoring to break up the large `main.cpp` file. This single file stored almost all of the codebase.

The refactoring focused on bringing in improvements to the chunk coordinate system and the hierarchy of game objects. Code from external projects were used as a point of reference to try to address the unique bugs that existed in the original codebase.

### State of the Codebase

As of the time of writing, the classic version of Blockycraft was difficult to built due to missing dependencies in the source code. This included requiring compiling an SDL audio library for supporting footsteps & other audio. Certain game assets have been 'zerod' out, as the source of these assets cannot be known. Based on these issues, I marked the project as archived.

The code is now available under `blockycraft-classic`. This includes screenshots of the project in a running state.

## Acknowledgements

I would like to take a moment to acknowledge worked that has been included or incorporated into the refactored version of the classic version of Blockycraft:

- [Perlin Noise](https://github.com/sol-prog/Perlin_Noise) - Replaced previous implementation with a modified version of sol-prog's improved perlin noise implementation
- [fogleman/Craft](https://github.com/fogleman/Craft) - Improved rendering mechanism and faster means of performing lookups of Chunk data (originally implemented was significantly slower)
- CS488 Course Assets - The project is built within the course assets of CS488. This has not changed in the refactoring

These acknowledgements are mirrored in the `blockycraft-classic` repository, but I felt it should be included here again.

### Kenney.nl

The project icon is retrieved from [kenney.nl](docs/icon/icon.json). The original source material has been altered for the purposes of the project. The icon is used under the terms of the [CC0 1.0 Universal](https://creativecommons.org/publicdomain/zero/1.0/).

The project uses assets by [Kenney from kenney.nl/](http://kenney.nl/assets/voxel-pack), and the icon is built from these assets.
