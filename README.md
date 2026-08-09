

<p align="center">
  <img src="docs/images/logo.png" width="256">
</p>

<h1 align="center">
Loot Search Speed
</h1>

<p align="center">
by <strong>moon11yy</strong>
</p>

---

![SPT](https://img.shields.io/badge/SPT-4.0.13-blue)
![License](https://img.shields.io/badge/License-MIT-green)
![C#](https://img.shields.io/badge/C%23-.NET%20Framework%204.7.2-purple)

## ✨ Features

- Adjustable **container & corpse search speed**
- Adjustable **item reveal speed** inside searched containers
- Preserves vanilla search mechanics
- Supports **Configuration Manager (F12)**
- Lightweight Harmony patches
- Open source

---

## ❌ What this mod does NOT change

- Unknown item examination speed
- Trader search
- Flea Market search
- Loot generation
- Search animations

---

## ⚙️ Configuration

The mod provides two independent settings.

### InitialSearchDelayMultiplier

Controls how long the initial **Searching...** animation lasts.

| Value |  Result   |
|-------|-----------|
| 1.0   | Vanilla   |
| 0.5   | 2× faster | 
| 0.25  | 4× faster |
| 0.0   | Instant   |

Default:

```text
0.5
```

---

### ItemRevealDelayMultiplier

Controls how quickly items appear inside an already opened container.

| Value |  Result   |
|-------|-----------|
| 1.0   | Vanilla   |
| 0.5   | 2× faster |
| 0.25  | 4× faster |
| 0.0   | Instant   |

Default:

```text
0.5
```

---

## 📦 Installation

1. Download the latest release.
2. Copy

```
moon11yy.LootSearchSpeed.dll
```

into

```
BepInEx/plugins/
```

3. Launch SPT.
4. Configure the mod through **F12** (Configuration Manager).

---

## ✅ Compatibility

- SPT **4.0.13**
- Client-side only
- Works with existing saves

---

## 🛠 Development

The project is open source and uses:

- C#
- .NET Framework 4.7.2
- Harmony
- BepInEx

Testing is recommended on a clean SPT installation before using large modlists.

---

## 📄 License

Licensed under the MIT License.

Copyright © 2026 moon11yy
