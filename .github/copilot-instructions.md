# GitHub Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

This mod aims to enhance specific game mechanics within RimWorld by introducing new functionalities and optimizations. The project is organized into several static and non-static classes, each dedicated to improving or adding features such as storage handling, item forbidding logic, reservation management, and ingestion behaviors. The mod integrates seamlessly with the existing game infrastructure, leveraging XML for configuration and Harmony for runtime patching of the game code.

## Key Features and Systems

### Key Features:
- **Storage Management**: Optimizations and extensions for storage-related functionality.
- **Item Interaction Restrictions**: Improved logic for determining when items should be forbidden.
- **Minification Enhancements**: New features for handling minified items.
- **Reservation Logic**: Enhancements to the mechanics determining item and space reservation.
- **Ingestion Surfaces**: Improvements to how characters find surfaces for eating.

### Systems:
- **Integration with Game Components**: Classes like `CompCanBelongToRoomOwners` help integrate mod features with room and ownership mechanics.
- **Harmony Patching**: The `HarmonyInit` class is used to apply patches at runtime, improving or altering game functionality.
  
## Coding Patterns and Conventions

- **Static Utility Classes**: Use `public static class` for singleton-style utility operations, such as in `ForbidUtility_IsForbidden`.
- **Inherit from Core Game Classes**: Non-static classes like `CompCanBelongToRoomOwners` extend RimWorld’s classes (e.g., `ThingComp`) to add or modify behaviors.
- **Method Naming**: Use descriptive and PascalCase names for methods to indicate their purpose and functionality clearly.

## XML Integration

- XML files are used to configure new entities and properties in the game world. Any new `CompProperties` are defined within XML to be loaded by the game at runtime, allowing for flexibility and dynamic updates without recompiling code.
- Ensure that XML is well-formed and follows the schemas expected by RimWorld for proper integration.

## Harmony Patching

- Utilize the `HarmonyInit` class for initializing Harmony patches, which non-destructively modify existing game methods. This allows for seamless integration and updating of game logic without directly altering the base game code.
- ***Best Practice***: Always perform thorough testing of patched methods to ensure compatibility and stability.

## Suggestions for Copilot

- **Code Suggestions for Static Classes**: Copilot can assist in generating boilerplate code for static utility methods, focusing on common patterns used in RimWorld modding.
- **Method Refinements**: For functions like reservation checks or minification logic, Copilot can help suggest conditional logic based on method inputs and expected game states.
- **XML Configuration**: Suggests XML snippets or templates based on syntax patterns used in RimWorld’s XML configurations, aiding in faster development.
- **Harmony Patching Patterns**: Copilot can suggest typical patterns for patching game methods with Harmony, including common prefix/postfix structures.

By adhering to these patterns and guidelines, developers can ensure that their mods are of high quality, stable, and integrated effectively within the RimWorld ecosystem.
