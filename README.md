# Cobra's Ammo Stack

![SPT](https://img.shields.io/badge/SPT-4.0.13-blue)
![Version](https://img.shields.io/badge/Version-1.0.0-green)
![License](https://img.shields.io/badge/License-MIT-yellow)

A lightweight server-side mod for **SPT 4.0.13** that gives you complete control over ammunition stack sizes.

Configure stack sizes by caliber, support ammunition from other mods, and override individual items using a simple JSON configuration.

---

# Features

- ✔ Configure stack sizes by caliber
- ✔ Support for vanilla ammunition
- ✔ Support for custom ammunition from other mods
- ✔ Exact item template overrides
- ✔ Configurable default stack size
- ✔ Startup information banner
- ✔ Lightweight server-side implementation
- ✔ No client-side installation required

---

# Compatibility

| Supported | Version |
|-----------|----------|
| SPT | 4.0.13 |

---

# Installation

1. Download the latest release.
2. Open the ZIP file.
3. Drag the **SPT** folder into your SPT installation directory.
4. Allow Windows to merge the folders.
5. Start the SPT Server.

If installed correctly you should see something similar to:

```text
============================================================
                    Cobra's Ammo Stack
                         Version 1.0.0
                     Built for SPT 4.0.13
============================================================

[OK] Loaded Successfully

[OK] Ammo Modified      : 218
[OK] Default Stack Size : 500
[OK] Known Calibers     : 26
[OK] Custom Calibers    : 7
[OK] Item Overrides     : 0

============================================================
```

---

# Configuration

All settings are located in:

```
config.json
```

Example:

```json
"Caliber556x45NATO": 500,
"Caliber545x39": 500,
"Caliber12g": 100,
"Caliber40x46": 50
```

---

## Default Stack Size

Used whenever an ammunition caliber is not found in either the **Calibers** or **CustomCalibers** sections.

```json
"DefaultAmmoStackSize": 500
```

---

## Custom Calibers

Supports ammunition added by other mods.

Example:

```json
"CustomCalibers": {
    "Caliber9x39": 500,
    "Caliber127x99": 100
}
```

---

## Item Overrides

Override a single ammunition item by its template ID.

```json
"ItemOverrides": {
    "ITEM_TEMPLATE_ID": 75
}
```

Item overrides always take priority over caliber settings.

---

# Logging

Enable detailed startup information.

```json
"ShowChangedAmmoInServerLog": false,
"ShowUnknownCalibersInServerLog": true
```

Unknown calibers will be displayed in the server console so they can be added to **CustomCalibers**.

---

# Tested

Version **1.0.0** has been tested with:

- Vanilla ammunition
- Shotgun shells
- VOG grenades
- 40mm grenade launcher rounds
- Modded ammunition
- 218 ammunition items

---

# Troubleshooting

### My ammunition stack sizes did not change.

- Make sure the mod is installed in:

```
SPT/user/mods/CobrasAmmoStack
```

- Check that the server startup banner appears.

- Make sure another mod is not changing ammunition stack sizes after Cobra's Ammo Stack loads.

---

### I see "Unknown caliber" messages.

Enable:

```json
"ShowUnknownCalibersInServerLog": true
```

Then copy the reported caliber into **CustomCalibers** and assign the desired stack size.

---

# Credits

Developer

**Cobra**


---

# Roadmap

## Version 1.1

- Automatic custom caliber detection
- Automatic configuration updates

## Version 1.2

- Improved statistics
- Better logging

## Version 2.0

- Optional graphical configuration editor

---

# License

Released under the MIT License.

See the included **LICENSE** file.

# Source Code

The complete source code and build instructions are available on GitHub:

https://github.com/CobraSnipper/CobrasAmmoStack
