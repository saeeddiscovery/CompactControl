# CompactControl

[!["Latest Release"](https://img.shields.io/badge/Release-v1.5.5-9cf.svg)](https://github.com/saeeddiscovery/CompactControl/releases/latest)
![Publish](https://github.com/saeeddiscovery/CompactControl/workflows/Publish/badge.svg)

## Digital remote control for Compact LINAC system

> This app would only work with our CompactControl board and interface

To publish the release automatically:
- Update CHANGELOG.md with the correct version and logs
- Commit changes
- Tag the last commit (Tag should be identical to Changelog Version):  
    ```> git tag v1.0.0 HEAD```
- push with tag:   
    ```> git push origin --tag v1.0.0```

- to delete a tag:    
```git tag -d v1.0.0```
- to delete a tag on the remote:  
```git push --delete origin v1.0.0```

