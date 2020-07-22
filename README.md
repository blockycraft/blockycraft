# Blockycraft

## Summary

Blockycraft is a Minecraft inspired Block Engine written in Unity3D and built using GitHub Actions. The project was first developed for a University of Waterloo graphics course, and this codebase is available as [blockycraft-classic](./classic/). This version has experienced refactoring after its original submission. I decided to rewrite the project into Unity3D as I still enjoyed the concept of block engines.

The [build pipeline](./deployment/) is done using GitHub Actions and the unity-builder actions. To avoid over-using the runners, the builds only trigger for pull requests, web deploys and releases. Using only GitHub services, it removes the need to manage deploy keys and infrastructure.

A playable copy is available at [blockycraft.jrbeverly.dev/play](https://blockycraft.jrbeverly.dev/play).
