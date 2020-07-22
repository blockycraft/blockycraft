# GitHub Actions Deployment Pipeline

The Blockycraft project is built using GitHub Actions, and the compiled artifacts are deployed with GitHub Releases & GitHub Pages. The advantage to this is no external dependencies exist in the pipeline. Although external services exist that could host these artifacts (itch.io), it is far easier to deploy these artifacts using only GitHub services.

The pipeline is composed of three types of runs:

- Pull Request: Run the build process, exposing build artifacts for demo in changeset reviews.
- Web Deploy: Deploy the project to GitHub Pages.
- Release: Build project artifacts and upload to GitHub Releases.

Details about these build processes are available in [.github/workflows/](.github/workflows), and documented to some degree below. A playable copy of the project is available at [blockycraft.jrbeverly.dev/play](https://blockycraft.jrbeverly.dev/play).

## Continuous Integration & Web Deploy

All pull requests trigger the build pipeline for the relevant target platforms. At this time, no tests are run. When running on the master branch, the process has an additional step of deploying the `WebGL` build to GitHub Pages.

These builds only run on pull requests due to the required build minutes per run. By only running on pull requests, it reduce the impact of builds running for small minor changes. Ideally there would be a more efficient way to cache build artifacts to reduce this (a la hermetic builds).

### Unity Upgrades

The build actions work with Unity under the hood, and as such need to be activated. The [upgrade process](upgrades.md) can be performed in a single pull request, but needs to be done in multiple commits. The `UNITY_LICENSE` secret that declares the license is versioned to allow avoid disrupting existing build runs.

The full process is documented under [deployment/upgrades](upgrades.md).

## Releases

Releases are created when a tag is pushed for a given commit. These tags should follow semantic versioning, but there is no requirement to do so. The intent is to have a bit of flexibility when making alterations to the deployment pipeline on a development branch. From a terminal, the release process is as follows:

```bash
TAG='vMAJOR.MINOR.PATCH' # v1.2.3
git tag $TAG master
git push origin $TAG
```

The release pipeline is responsible for creating the GitHub Release, and only the tag needs to be created. If you wish to do so from the GitHub interface, you can create then delete a release. This will trigger a pipeline run for the newly created tag, and the release delete will prevent clashing. The release will then be "re-created" with the compiled artifacts attached as zip files.
