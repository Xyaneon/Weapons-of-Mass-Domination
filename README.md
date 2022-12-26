# Weapons of Mass Domination
A console game for evil geniuses. Try to take over the world! :earth_americas: :boom:

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/Xyaneon/Weapons-of-Mass-Domination/blob/master/LICENSE)
![Workflow](https://github.com/Xyaneon/Weapons-of-Mass-Domination/actions/workflows/dotnet.yml/badge.svg)

**:construction: This game is still under development.**

## :video_game: Premise

You and your opponents are evil geniuses attempting world domination. Manage money, henchmen and reputation to take control of land area; the
first player to control the entire land surface area of Earth wins!

Along the way, beware of attacks from other players, and try to get your hands on a nuclear stockpile to turn the tide in your favor.

## :book: About

### :floppy_disk: Background info

As a high school student still new to programming, I created a command line turn-based strategy game called WMD: Weapons of Mass Domination.
The original implementation used the Borland IDE and C++, and was stored and distributed on floppy disks. You can find the original source
code archived [here](https://github.com/Xyaneon/WMD-Weapons-of-Mass-Domination-Original).

Today, I hold a Master's degree in Computer Science and work as a professional software developer.
This is a newer re-implementation of that game from many years ago, built using modern technologies and development practices I have learned since then.
This is basically a fun hobby project of mine to see how far along my skills and knowledge have progressed since then (and also because some
of my friends still ask if I can make the game available to them again).

### :sparkles: Improvements and new features

This new version of the game has the following improvements over the original:
- [.NET 6](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6) and [C# 9](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9)
- Separate assemblies (i.e., core game logic is separate from the command line app)
- Some unit testing
- Better game state management with OOP, commands, and immutable [`record`](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-9#record-types) types
- UI improvements, including using arrow keys on command menus rather than entering numbers
- More realistic Earth statistics

### ❓ Frequently-Asked Questions

See [here][faq] for a list of frequently-asked questions about this project.

## :scroll: License

This project is open-source and provided under the MIT license. You can view it in the [LICENSE](https://github.com/Xyaneon/Weapons-of-Mass-Domination/blob/master/LICENSE) file.


[faq]: https://github.com/Xyaneon/Weapons-of-Mass-Domination/issues?q=is%3Aissue+label%3A%22%3Aquestion%3A+faq%22
