<h1 align="center">
  <a href="https://github.com/Blockycraft/Blockycraft" title="Blockycraft">
    <img alt="Blockycraft" src="./logo.png" width="200px" height="200px" />
  </a>
  <br />
  Blockycraft
</h1>

<p align="center">
  Blockycraft is an interactive demo that uses standard first person controls to navigate through a block world
</p>

<br />

## Summary

Blockycraft is a interactive graphics demo to create a Minecraft inspired demo which revolves around breaking and placing blocks. The game world is composed of rough cubes arranged in a fixed grid pattern and representing different materials, such as dirt, stone, and snow.  The techniques used in the demo can be toggled using keyboard commands.  The Blockycraft project is written using C++ and OpenGL.

![blockycraft world](./logo.png "Blockycraft")

## Notes

The project was developed for a University of Waterloo Graphics course in Summer 2016. The focus was on graphics techniques (e.g. transparency, ambient occlusion) or gameplay.

The original blockycraft project was a hack & slack project built under a pretty short timeline (~month). After completing it, the project went through a second round of refactoring to break up the `main.cpp` file which stored almost all of the codebase. During this refactoring, improvements were pulled in, or code from external projects was adopted to improve the overall codebase.

Due to the difficulty of building the project, I recently recorded video of the project to go along with the screenshots.

## Acknowledgements

I would like to take a moment to acknowledge worked that has been included or incorporated into the refactored version of Blockycraft:

- [Perlin Noise](https://github.com/sol-prog/Perlin_Noise) - Replaced previous implementation with a modified version of sol-prog's improved perlin noise implementation
- [fogleman/Craft](https://github.com/fogleman/Craft) - Improved rendering mechanism and faster means of performing lookups of Chunk data (originally implemented was significantly slower)
- CS488 Course Assets - The project is built within the course assets of CS488. This has not changed in the refactoring

## Kenney.nl

The project icon is retrieved from [kenney.nl](docs/icon/icon.json). The original source material has been altered for the purposes of the project. The icon is used under the terms of the [CC0 1.0 Universal](https://creativecommons.org/publicdomain/zero/1.0/).

The project uses assets by [Kenney from kenney.nl/](http://kenney.nl/assets/voxel-pack), and the icon is built from these assets.
