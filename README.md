# Cobra's Ammo Stack

A lightweight server-side mod for **SPT 4.1.x** that increases the maximum stack size of ammunition using a simple and fully configurable `config.json`.

Designed to be fast, reliable, and easy to customize.

---

## Features

- Supports **SPT 4.1.x**
- Lightweight server-side mod
- Configure stack sizes by caliber
- Item-specific stack overrides
- Supports custom calibers
- Simple JSON configuration
- Minimal startup logging
- No additional dependencies

---

## Installation

1. Download the latest release.
2. Extract the included **SPT** folder into your SPT installation directory.
3. Allow Windows to merge folders if prompted.
4. Start the SPT Server.

Your installation should look like:

```
SPT
└── user
    └── mods
        └── CobrasAmmoStack
            ├── CobrasAmmoStack.dll
            ├── config.json
            ├── README.md
            ├── CHANGELOG.md
            └── LICENSE
```

---

## Configuration

All settings are located in:

```
config.json
```

You can customize:

- Default ammunition stack size
- Individual caliber stack sizes
- Custom caliber stack sizes
- Item-specific overrides
- Enable or disable the mod

No code changes are required.

---

## Default Stack Sizes

| Category | Default Stack Size |
|----------|-------------------:|
| Pistols / SMGs | 500 |
| Rifles | 500 |
| Shotguns | 100 |
| 40mm Grenade Launcher Rounds | 50 |
| 26x75 Signal Cartridges | 25 |

---

## Custom Ammo Support

Custom ammunition that uses existing Tarkov calibers is automatically supported.

Mods that introduce entirely new calibers can be configured by adding them to the `CustomCalibers` section of `config.json`.

---

## Compatibility

Compatible with:

- SPT 4.1.x

This mod modifies ammunition stack sizes only and does not replace or overwrite any vanilla items.

---

## Logging

Startup logging is intentionally minimal.

Example:

```
[Cobra's Ammo Stack] v1.1.0 loaded. Modified 210 ammunition items.
```

---

## License

Released under the MIT License.

---

## Author

**Cobra**

GitHub:

https://github.com/CobraSnipper/CobrasAmmoStack