# Projektový deník (MEMORY)
Zde se sleduje aktuální stav projektu, implementované moduly a seznam úkolů (Pending Tasks).

## Aktuální stav
- Architektura používá **Singleton Services** (přístup přes `instance`).
- DI systém a `BaseMonoBehaviour` kompletně odstraněny.
- Klíčové služby: `GameStateService`, `PowerGridService`, `HUDService`.
- `CharacterHealth` také funguje jako Singleton.
- `HullSection` a `SeaCreature` jsou běžné MonoBehaviour komponenty.
- Nástroj `GeminiBridge` (editorový skript pro automatizaci scény) odstraněn dle požadavku. Preferujeme self-inicializaci přes `[GlobalInit]` atribut.
- Poškození `HullSection` nyní ovlivňuje globální integritu ponorky v `GameStateService`.
- `Reactor` nyní spotřebovává palivo z `GameStateService.instance.fuelLevel`.
- `HUDService` nyní zobrazuje palivo a hloubku z `GameStateService`.
- **Input System:** Všechny skripty (`PlayerController`, `InteractionSystem`, `SteeringTerminal`, `Welder`) migrovány na nový **Input System package** (použití `UnityEngine.InputSystem`).

## TODO
- [ ] Vytvořit `SubmarineManager` pro globální správu všech sekcí trupu.
