# Mobile Survivors: Performance-Driven Horde Survival

**Portfolio Page:** https://sites.google.com/view/nicholasgrossportfoliopage/developer-bio/mobile-survivors

---

## Demo Overview
Mobile Survivor is a technical demo and practice project modeled after the core mechanics of *Vampire Survivors* by Luca Galante. The goal of this project was to use an established horde-survival loop as a sandbox to implement professional coding techniques, including Event-Driven Architecture, Interface-based Strategy Patterns, and Mobile Optimization. The framework is engineered to be a scalable combat system capable of maintaining high performance on mobile hardware while managing dozens of concurrent enemy entities.

## Tools & Technologies
| Category | Technology |
| :--- | :--- |
| **Engine** | Unity 6000.0.36f1 |
| **Input** | New Input System (Mobile Touch Optimization) |
| **UI System** | Unity UI Toolkit (Modern UXML/USS workflow) |
| **Architecture** | Strategy Pattern & Observer Pattern |

## Project Architecture
The project utilizes advanced design patterns to decouple game logic from the user interface and system controllers, ensuring the game remains performant even with hundreds of active entities.

### Core Systems & Logic
* **PlayerCharacter.cs:** The central data hub for the player. It manages the experience (EXP) system and level-up progression using C# events to notify other systems without tight coupling.
* **PlayerMovement.cs:** Handles mobile-optimized physics, interfacing with the InputHandler to translate screen-space touch data into frame-rate independent movement via Rigidbody2D.
* **InputHandler.cs:** Bridges Unity's New Input System with gameplay logic by normalizing screen-space touch data into world-space vectors.
* **Health.cs:** A health management system utilizing the Observer Pattern. By exposing C# Actions (`OnHealthChanged`), it allows the UI and effects systems to react to damage instantly.
* **EnemySpawner.cs:** Manages encounter intensity over time using a rate-decay algorithm to ensure a consistent challenge curve.

### Weaponry & Progression
The project utilizes a **Strategy Pattern** for player growth, ensuring modular scalability.
* **IUpgrade.cs:** The core interface defining the contract for all player improvements, from stat boosts (MoveSpeedUpgrade) to new weapon archetypes (MagicWandUpgrade).
* **WeaponSystem.cs & WeaponData.cs:** A data-driven combat system. Using ScriptableObjects, the system instantiates projectiles or melee attacks based on defined parameters like detection radius and cooldowns.
* **Projectile.cs:** Manages auto-targeting and physics-based movement for ranged attacks to ensure reliable enemy tracking.

## Key Technical Highlights

### Performance-Oriented UI
The `GameHUDController.cs` uses logic-view decoupling. The HUD only updates visual elements, such as EXP and Health bars, when specific events are triggered. Utilizing the **UI Toolkit** ensures a resolution-independent layout that scales across various mobile aspect ratios.

### Tactile Mobile Controls
Raw touchscreen input is converted into normalized direction data and paired with kinematic physics movement. This approach prevents "tunneling" and ensures reliable collision detection even during high-density combat sequences.

### Efficient Resource Management
* **SpawnOnDeath.cs:** A utility script that handles the instantiation of loot or effects, interfacing with the Health event system for clean execution.
* **AudioHelper.cs:** A static utility that handles 2D audio instantiation and automatic cleanup.

## Learning Outcomes: Technical Adaptability
This project served as a case study in **Mobile Resource Management**. By implementing an event-driven workflow and an extensible upgrade framework, I prioritized system scalability and performance over tightly coupled code structures.
