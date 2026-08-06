# Changelog

All notable changes to this project will be documented in this file.

---

## [1.1.0] - 2026-08-05

### Added
- Full support for SPT 4.1.x
- Support for the new SPT 4.1 mod metadata system (`IModMetadata`)
- Configurable stack sizes by caliber
- Support for custom calibers
- Item-specific stack size overrides
- Minimal startup logging

### Changed
- Rebuilt the mod using the SPT 4.1.x API
- Updated the project to .NET 10
- Migrated from the legacy SPT 4.0 metadata system
- Updated grenade projectile caliber mappings for SPT 4.1.x
- Improved project structure and documentation

### Fixed
- Fixed compatibility with SPT 4.1.x
- Fixed loading with the new server mod loader
- Fixed VOG-25 caliber detection (`Caliber40mmRU`)
- Fixed startup and dependency injection for the new API

---

## [1.0.1] - 2026-07-23

### Changed
- Improved startup logging.
- Added support for additional ammunition calibers.
- Minor code cleanup.

---

## [1.0.0] - 2026-07-23

### Initial Release
- Initial public release.
- Configurable ammunition stack sizes.
- JSON-based configuration.
- Support for caliber-specific stack sizes.
- Item-specific overrides.