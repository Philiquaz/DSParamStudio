## About DS Param Studio:
DS Param Studio is a dark souls param editor for all the souls games. It is forked from DSMapStudio, such that future integration between map and params may be possible. It is currently in a concept phase, as its future independent of DSMapStudio is uncertain.

## Basic usage instructions
These are the same as for DSMapStudio.
### Game instructions
* **Dark Souls Prepare to die Edition**: Game must be unpacked with UDSFM before usage (https://www.nexusmods.com/darksouls/mods/1304).
* **Dark Souls Remastered**: Not officially supported by MapStudio, but I presume params are unchanged and work fine.
* **Dark Souls 2 SOTFS**: Use UXM (https://www.nexusmods.com/sekiro/mods/26) to unpack the game. ParamStudio is capable of decrypting params. Vanilla Dark Souls 2 is not supported.
* **Dark Souls 3 and Sekiro**: Use UXM to extract the game files.
* **Demon's Souls**: Tested against the US version, but any valid full game dump of Demon's Souls will probably work out of the box. Make sure to disable the RPCS3 file cache to test changes if using the emulator.
* **Bloodborne**: Any valid full game dump should work out of the box. Note that some dumps will have the base game (1.0) and the patch as separate, so the patch should be merged on top of the base game before use. You're on your own for installing mods to console at the moment.

### Mod projects
Param Studio uses MapStudio's projects, capable of saving to other directories intended for use with modengine.

## FAQ
### Q: Why use this over yapped, soulstruct, paramvessel, etc?
The existing tools all do their core job - they display and allow editing of a bunch of numbers. Some, like yapped honeybear edition, even let you declare some fields as enums, letting you use names instead of numbers, which can be useful. Param Studio goes a step further, while fundamentally maintaining the classic param-editor format.
Features include:
* Multiple Views into the same params - avoids having multiple copies open when copying/reading around
* Powerful row searching features - can perform complicated searches like "all rows where a certain field is not 0"
* Param fields may be marked with several piece of data
    * Enums - a set of values that a field should take, readily selectable and readable
    * References - when a value refers to another row, that row's name is displayed and can be quickly navigated to, and a value can be set by searching for a name. (It can even alias to the nearest 100 or so).
    * Virtual references - a value shared among multiple params, which does not specify a param row, but for which navigation to other occurances is desirable.
    * Alternative names - DS Param Studio relies on commonly shared paramdefs, which define what a param is made up of, and give each field a name. Sometimes, these names aren't ideal, even if they're authentic.
    * Wiki entries - small tooltips attached to a field to provide a quick reference for the user before resorting to a real wiki or asking in a discord.
* Params may be given an alternative order for display, allowing certain misplaced fields to return to their families. Separators can be included also for cleanliness' sake.
* Mass editing features including:
    * A syntax for selecting fields and setting their value and performing basic arithmetic on them, using constants or values from other fields
    * CSV import / export for more complicated edits, and for possible version control.
* Game hooks for DS3 including
    * Reloading params in a running game (excluding row addition/deletion)
    * Itemgib

### Q: Is this the end of DSMapStudio's param editor?
A: No, this is more of a stopgap measure for providing features to modders. DSMapStudio is not currently being developed and I am not in a position to make official releases. Rather than release stuff with Kata's name on it and have him be pinged over random bugs he is uninvolved with, this should allow me to make tools and give them to people. Since this was original developed inside DSMapStudio, it should remain compatible and updates should be readily portable to DSMapStudio.

### Q: Graphics requirements?
A: DSMapStudio creates a whole vulkan renderer, and requests all sorts of features. I'm sat on the same renderer, even if many of those features are not used. With work it may be possible to reduce these requirements, but that would be thrown away when reintegrated with DSMapStudio.

## System Requirements:
* Windows 7/8/8.1/10 (64-bit only)
* [Microsoft .Net Core 3.1 **Desktop** Runtime](https://dotnet.microsoft.com/download/dotnet-core/3.1)
* [Visual C++ Redistributable x64 - INSTALL THIS IF THE PROGRAM CRASHES ON STARTUP](https://aka.ms/vs/16/release/vc_redist.x64.exe)
* **A Vulkan Compatible Graphics Device with support for descriptor indexing**, even if you're just modding DS1: PTDE
* Intel GPUs currently don't seem to be working properly. At the moment, you will probably need a somewhat recent (2014+) NVIDIA or AMD GPU.

## Troubleshooting:
* If the program fails to start, the primary causes are .Net Core 3.1 Desktop Runtime and Visual C++ Redistributable. Repair or reinstall these if you're having problems.
* If the program begins to launch and then fails (eg. white screen), try running as an administrator. If that fails, make sure your computer is using the correct graphics device for the program if you have integrated graphics.
* If you are unable to start or open a project, chances are your paramdefs or game install is wonky or misaligned. Support is not given for cracked games. For sekiro, attempt updating paramdefs.

## Special Thanks
* Katalash - Made DSMapStudio
* TKGP - Made Soulsformats
* [Pav](https://github.com/JohrnaJohrna)
* [Meowmaritus](https://github.com/meowmaritus) - Made DSAnimStudio, which DSMapStudio is loosely based on
* [PredatorCZ](https://github.com/PredatorCZ) - Reverse engineered Spline-Compressed Animation entirely.
* [Horkrux](https://github.com/horkrux) - Reverse engineered the header and swizzling used on non-PC platform textures.
* [Vawser](https://github.com/vawser) - DS2/3 Documentation

## Libraries Utilized
* Soulsformats
* [Newtonsoft Json.NET](https://www.newtonsoft.com/json)
* Veldrid for rendering
* ImGui.NET for UI
* A small portion of [HavokLib](https://github.com/PredatorCZ/HavokLib), specifically the spline-compressed animation decompressor, adapted for C#
* Recast for navigation mesh generation
* Fork Awesome font for icons
