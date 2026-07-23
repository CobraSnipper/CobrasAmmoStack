# Changelog

All notable changes to this project will be documented in this file.

This project follows Semantic Versioning (SemVer).

---

## [1.0.0] - 2026-07-23

### 🎉 Initial Release

This is the first public release of **Cobra's Ammo Stack** for SPT.

### Added

- Configurable ammunition stack sizes by caliber
- Support for vanilla SPT ammunition
- Support for custom ammunition calibers from other mods
- Individual item template overrides
- Configurable default stack size for unknown calibers
- Startup summary banner
- Optional logging of modified ammunition
- Optional logging of unknown calibers
- Lightweight server-side implementation
- Drag-and-drop installation
- Full compatibility with SPT 4.0.13

### Priority Order

Stack sizes are applied in the following order:

1. Item Overrides
2. Standard Calibers
3. Custom Calibers
4. Default Stack Size

### Tested

Successfully tested with:

- 218 ammunition items
- Rifle ammunition
- Pistol ammunition
- Shotgun shells
- Grenade launcher rounds (40mm)
- VOG grenades
- Vanilla SPT items
- Modded ammunition

### Performance

- Fast startup processing
- Server-side only
- No client installation required
- Minimal performance impact

---

## Future Releases

### Planned for v1.1.0

- Automatic detection of custom ammunition calibers
- Automatic configuration updates for newly detected calibers
- Additional startup statistics
- Improved logging

### Planned for v1.2.0

- Enhanced reporting
- More configuration options
- Additional quality-of-life improvements