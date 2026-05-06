# Submarine Journey (3D Barotruma-like) - Project Instructions

## Project Overview
A 3D submarine simulator focused on internal systems management, crew interaction, and survival.

## Technical Stack
- **Engine:** Unity 2022.3+ (URP)
- **Language:** C#
- **Patterns:** Component-based, Interface-driven interactions (`IInteractable`).

## Coding Standards
- **Namespaces:** Always use `SubmarineJourney.<Module>`.
- **Naming Conventions:**
  - Classes/Methods: `PascalCase`
  - Private Fields: `camelCase` (e.g., `private float currentHealth`)
  - Parameters: `aPrefix` (e.g., `float aDamage`) - *based on existing codebase*
- **Dependencies:** Use `[SerializeField]` for Inspector assignment. Use `Object.FindFirstObjectByType<T>()` sparingly; prefer registration patterns (see `PowerGrid`).

## Architecture Decisions
- **Systems:** Modular components (Reactor, Engine, OxygenGenerator) that register themselves to a controller (e.g., `PowerGrid`).
- **Submarine:** Divided into `HullSection` objects for localized damage and flooding.
- **Interactions:** Raycast-based using `IInteractable` on the target object.

## Workflows
- Always update `MEMORY.md` after significant changes.
- Ensure new systems are power-aware (inherit from or use `PowerConsumer`).
