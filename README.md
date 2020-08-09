# blockycraft

## Summary

Blockycraft is a Minecraft inspired Block Engine written in Unity3D and built using GitHub Actions. The intent of this project is to better learn Unity, and discover some new use cases with GitHub Actions.

The project is available as binary releases for different operating systems, and includes a hosted WebGL version that can be played in supported browsers:

[blockycraft.jrbeverly.dev/play](https://blockycraft.jrbeverly.dev/play)

There is no formal feature list or any intentions to carry the project long-term into a fully featured block engine.

## Build Pipeline

The [build pipeline](./docs/deployment/) is done using GitHub Actions and the unity-builder actions. To avoid over-using the runners, the builds only trigger for pull requests, web deploys and releases. Using only GitHub services, it removes the need to manage deploy keys and infrastructure.

A playable WebGL copy is available at [blockycraft.jrbeverly.dev/play](https://blockycraft.jrbeverly.dev/play), and the binaries for the project are available on the [releases tab.](https://github.com/blockycraft/blockycraft/releases)

## Acknowledgements

The project assets were created by [kenney.nl](http://kenney.nl/assets/voxel-pack) and available for download by others. The game assets are used under [CC0 1.0 Universal](https://creativecommons.org/publicdomain/zero/1.0/).

The project icon uses assets by [Kenney from kenney.nl/](http://kenney.nl/assets/voxel-pack). The game assets have been combined together to produce the output.
