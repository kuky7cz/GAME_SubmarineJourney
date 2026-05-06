# Projektový deník (MEMORY)
Zde se sleduje aktuální stav projektu, implementované moduly a seznam úkolů (Pending Tasks).

## Aktuální stav
- Architektura používá **Singleton Services** (přístup přes `instance`).
- DI systém a `BaseMonoBehaviour` kompletně odstraněny.
- Klíčové služby: `GameStateService`, `PowerGridService`, `HUDService`.
- `CharacterHealth` také funguje jako Singleton.
- `HullSection` a `SeaCreature` jsou běžné MonoBehaviour komponenty.

## TODO
- [ ] Propojit `HullSection.TakeDamage` s `GameStateService.instance.TotalSubmarineIntegrity`.
- [ ] Implementovat logiku pro spotřebu paliva v `Reactor.cs` (odečítat z `GameStateService.instance.FuelLevel`).
- [ ] Upravit `HUDService`, aby zobrazoval hloubku a palivo z `GameStateService`.
- [ ] Vytvořit `SubmarineManager` pro globální správu všech sekcí trupu.
