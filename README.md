# CompactControl

[!["Latest Release"](https://img.shields.io/badge/Release-v1.5.5-9cf.svg)](https://github.com/saeeddiscovery/CompactControl/releases/latest)
![Publish](https://github.com/saeeddiscovery/CompactControl/workflows/Publish/badge.svg)

## Digital remote control for Compact LINAC system

> This app would only work with our CompactControl board and interface

-------------------------
## Publish the Release Automatically:

1. Set the proper version in the Visual Studio.
    - Project (menu) -> CompactControl Properties... -> Application (tab) -> Assembly Information... (button)
2. Update ```ChangeLog.md``` with the correct version and change logs.
    - version MUST be in this format: ```1.1.1-rc.1``` (Change the first 3 numbers according to the version set in the Visual Studio)
3. Commit changes.
4. Tag the last commit. (Tag MUST be identical to the ChangeLog's version):  
    ```> git tag v1.1.1-rc.1 HEAD```
5. Push with the same tag:   
    ```> git push origin --tag v1.1.1-rc.1```

-------------------------
### Delete a tag from local repository: 
```> git tag -d v1.1.1-rc.1```
### Delete a tag from remote repository: 
```> git push --delete origin v1.1.1-rc.1```

