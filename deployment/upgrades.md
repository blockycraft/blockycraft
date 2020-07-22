# Unity Version Upgrading

The process for upgrading the Unity version of the project is as follows:

1) Increase the version in  `.github/workflows/activation.yml` to the latest version
2) Raise a pull request with the change, confirming the intent to upgrade
3) Download the license file from GitHub Artifacts and upload to Unity
4) Import the license as a GitHub Secret named for the version: `2019.4.3f1 => UNITY_LICENSE_2019_4_3f1`
5) Update references of the license & unity version in `.github/workflows/activation.yml`
6) Confirm the project builds successfully, and merge the change.

If there exists any previous `UNITY_LICENSE_` secrets that are older than 2 versions, you can remove them from the project. Points of reference for this are the [unity docs](https://unity-ci.com/docs/github/activation) and the [unity-activate](https://github.com/marketplace/actions/unity-activate) github action.

## Unity License

The unity license is stored in an environment variable named `UNITY_LICENSE_{VERSION}`, where `{VERSION}` refers to the Unity version of the project. For the version `2019.4.3f1`, the secret should follow the naming convention of `2019_4_3f1`.

It is advisable to keep at least 2 `UNITY_LICENSE_*` available, to allow for quick downgrading in the event of issues with the upgrade process.
