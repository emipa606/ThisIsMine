# GitHub Copilot Instructions for "This Is Mine (Continued)"

## Mod Overview and Purpose

**Mod Name:** This Is Mine (Continued)  
**Author:** El Luchador (Updated by other contributors)  
**Description:** This mod enhances the control players have over their colony by allowing specific furniture pieces to be assigned to individual pawns via bed ownership. By doing so, it prevents pawns from using furniture that has been specifically designated for another pawn's use. This is particularly useful for managing resources and ensuring efficient use of space within the colony. The mod has been updated to fix certain features and provide a better user experience.

## Key Features and Systems

- **Ownership Reset:** Furniture that gets moved or minified will now automatically reset their ownership to avoid conflicts.
- **Hotkey Update:** Based on user feedback, certain hotkeys have been changed for better usability.
- **Icon Update:** Icons have been refreshed by Soul, improving visual clarity. In case of any distortion issues, users are advised to disable texture compression in the game settings.

### Compatibility

- The mod interacts seamlessly with vanilla defs and is designed to be compatible with other mods that adhere to proper labeling conventions. Specific compatibility is noted with LWM's Deep Storage and RimFridge.
- The mod includes error-diagnosing tips and support channels for users to troubleshoot issues effectively.

## Coding Patterns and Conventions

The code structure is organized into several key types that dictate the behavior of various components of the mod:

- **CompCanBelongToRoomOwners.cs:** Handles gizmo logic and visual selection overlays.
- **Harmony Patches:** Various C# files under `HarmonyPatches` directory employ Harmony to patch the game's behavior:
  - **MinifyUtility_MakeMinified.cs**
  - **ReservationManager_CanReserve.cs**
  - **Toils_Ingest_FindAdjacentEatSurface.cs**
  - **ForbidUtility_IsForbidden.cs**

### XML Integration

- The mod includes a single XML file: **About.xml**. It depends on the brrainz.harmony library and contains essential metadata for the mod's integration with RimWorld's engine.

## Harmony Patching

The mod relies heavily on Harmony patches to adjust the game's behavior:

- **Postfix and Prefix Methods:** These methods allow the mod to alter the execution of game functions at specific points.
- **Examples:**
  - Modifying reservation checks to account for new ownership rules.
  - Adjusting ingest behavior so that pawns respect furniture assignments.

## Suggestions for Copilot

To improve development workflow using GitHub Copilot, consider the following suggestions:

- **Pattern Recognition:** Leverage Copilot's ability to learn patterns from the existing codebase to suggest functions for new features or other mods' compatibility.
- **Documentation Generation:** Ensure that all C# methods have ample XML comments to enable Copilot to better understand the context and generate useful comments or documentation automatically.
- **Code Refactoring:** Use Copilot suggestions to refactor older code segments for optimization, readability, and maintenance purposes.
- **XML Syntax Assistance:** Utilize Copilot to assist with XML syntax corrections and enhancements, especially for mod's metadata configurations.
- **Dynamic Mod Compatibility Lists:** Enable Copilot to help create scripts or methods for dynamically generating compatibility lists based on user comments or feedback.

This structured approach with GitHub Copilot can streamline ongoing development and maintenance of the "This Is Mine" mod, aiding both current and future contributors in enhancing or expanding its features.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

