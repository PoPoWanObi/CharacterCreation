# CharacterCreation
Detailed Character Creation

Libraries currently in use:

https://github.com/Aragas/Bannerlord.MBOptionScreen


https://github.com/pardeike/Harmony

## Build Instructions
### Requirements
- .NET Framework 4.7.2
- .NET 6
- A usable IDE (recommended Visual Studio or JetBrains Rider)
- Environmental variable BANNERLORD_GAME_DIR defined

### Procedure
1. Pull this repository and open the solution.
2. Compile the procedure.
   1. It's fine to use "Build Solution" at first build. Subsequent builds should use "Rebuild Solution"
   due to the specific order the solution should be built.
3. Launch the game and enable the mods.

### For Release Build
- Ensure to create symbolic links (junction-type) pointing to `zzCharacterCreation` and `zzCharacterCreation_AdditionalSliders`
in the `Modules` folder.
- Build the release by selecting `fomod`, `Modules`, `Image_Main.png`, and `Image_Sliders.png` and archiving them to
`CharacterCreationFOMOD.zip`.
  - The symbolic links will be followed automatically, and the final archive will include both mods as though they
  actually existed at `Modules`.