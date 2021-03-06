# Blockycraft

## Summary

Blockycraft is a Minecraft inspired Block Engine written in Unity3D and built using GitHub Actions. The intent of this project is to better learn Unity, and discover some new use cases with GitHub Actions.

The project is available as binary releases for different operating systems, and includes a hosted WebGL version that can be played in supported browsers:

[blockycraft.jrbeverly.me/play](https://blockycraft.jrbeverly.me/play)

There is no formal feature list or any intentions to carry the project long-term into a fully featured block engine.

## History

The Blockycraft project was first developed for a University of Waterloo graphics course, and this codebase is available as [blockycraft-classic](./classic/). The first codebase went through some revisions in an attempt to address the technical debt incurred in the original development cycle for the demo. While these did make some improvements, they still left the codebase in a less than ideal state.

With the introduced of GitHub Actions, and a recent spark of interest in using Unity, I looked to rebuild parts of the project in Unity. The project isn't intended to be feature complete with the original blockycraft, but instead be a enjoyable project for [tinkering with unity](./unity/).

## Build Pipeline

The [build pipeline](./deployment/) is done using GitHub Actions and the unity-builder actions. To avoid over-using the runners, the builds only trigger for pull requests, web deploys and releases. Using only GitHub services, it removes the need to manage deploy keys and infrastructure.

A playable WebGL copy is available at [blockycraft.jrbeverly.me/play](https://blockycraft.jrbeverly.me/play), and the binaries for the project are available on the [releases tab.](https://github.com/blockycraft/blockycraft/releases)

## Controls

The project can be interacted with in the following ways:

|**Action**|**Description**|
|---|---|
|Esc| Undock from the application|
|Left Shift | Decrease the height of the player |
|Space | Increase the height of the player |
|Left Click| Destroy a block at the highlighted location |
|Right Click| Place the block at the identified location |
|Mouse Wheel (Alt: Q/E)| Cycle through the available block types for placement |

### Kenney.nl

The project assets were created by [kenney.nl](http://kenney.nl/assets/voxel-pack) and available for download by others. The game assets are used under [CC0 1.0 Universal](https://creativecommons.org/publicdomain/zero/1.0/).

The project icon uses assets by [Kenney from kenney.nl/](http://kenney.nl/assets/voxel-pack). The game assets have been combined together to produce the output.
