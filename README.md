# KSL_API_Extensions

A collection of C# features that extend the KSL API in **CarX Drift Racing Online**.

## Building

The repository contains an SDK style project file. Build with:

```bash
dotnet build
```

You will need references to the game assemblies such as `CarX.dll`, `UnityEngine.dll`, `DB.dll` and `DI.dll`. Point the `ReferencePath` MSBuild property to the folder containing these assemblies or edit the project file accordingly.

## Overview

The code is organized into `Extensions` subfolders:

- `Abstractions` – base interfaces and classes for mod features
- `Composition` – feature registry and lifecycle management
- `Facade` – wrappers over game state
- `Features` – built‑in system features
- `Integration` – Harmony patch hooks
- `Logic` – utility classes and implementations
- `Runtime` – runtime state trackers


