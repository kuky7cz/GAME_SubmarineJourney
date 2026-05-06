# Instrukce k projektu Submarine Journey (3D Barotruma)

## Přehled projektu
3D simulátor ponorky zaměřený na správu interních systémů, interakci posádky a přežití.

## Technický stack
- **Engine:** Unity 2022.3+ (URP)
- **Jazyk:** C#
- **Vzory:** Komponentově orientovaný, interakce přes rozhraní (`IInteractable`).

## Standardy kódování
- **Jmenné prostory:** Vždy používat `SubmarineJourney.<Modul>`.
- **Konvence pojmenovávání:**
  - Třídy/Metody: `PascalCase`
  - Soukromá pole: `camelCase` (např. `private float currentHealth`)
  - Parametry: `aPrefix` (např. `float aDamage`) - *podle stávajícího kódu*
- **Závislosti:** Používat `[SerializeField]` pro přiřazení v Inspectoru. `Object.FindFirstObjectByType<T>()` používat šetrně; preferovat registrační vzory (viz `PowerGrid`).

## Architektonická rozhodnutí
- **Systémy:** Modulární komponenty (Reaktor, Motor, Generátor kyslíku), které se samy registrují do ovladače (např. `PowerGrid`).
- **Ponorka:** Rozdělená na objekty `HullSection` pro lokální poškození a zaplavování.
- **Interakce:** Založené na Raycastu s využitím `IInteractable` na cílovém objektu.

## Pracovní postupy
- Po významných změnách vždy aktualizovat `MEMORY.md`.
- Zajistit, aby nové systémy braly v úvahu napájení (dědit z `PowerConsumer` nebo ho používat).
