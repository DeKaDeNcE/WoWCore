## WoWCore

[WoWCore](https://github.com/DeKaDeNcE/WoWCore) is a open source fork of [CypherCore](https://github.com/CypherCore/CypherCore).

[CypherCore](https://github.com/CypherCore/CypherCore) is a C# open source port of [TrinityCore](https://github.com/TrinityCore/TrinityCore).

The current supported game version is: 10.0.7.49343

### Cloning the repo

Use this command:

`git clone https://github.com/DeKaDeNcE/WoWCore.git`

### Pulling changes from the repo

Sometimes we force push to the master branch, because we always rebase it on CypherCore to keep Git history extremely clean, so it's best if you want to pull latest changes to use this command:

`git fetch && git reset origin/master --hard`

### Compiling code

For Windows Use this command:

`dotnet build --runtime win-x64 -p:ImportByWildcardBeforeSolution=false`

For Linux Use this command:

`dotnet build --runtime linux-x64 -p:ImportByWildcardBeforeSolution=false`

For Mac Use this command:

`dotnet build --runtime osx-x64 -p:ImportByWildcardBeforeSolution=false`

### Prerequisites
* [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* [MySQL 8.0+](https://dev.mysql.com/downloads/mysql/) or [MariaDB 10.6+](https://mariadb.org/download/)
* Optional: [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/), [Visual Studio Code](https://code.visualstudio.com/download) or [JetBrains Rider](https://www.jetbrains.com/rider/download/#section=windows)

### Server Setup
* You can download already extracted data from [Arctium](https://tc.arctium.io/master/)
* Or you can use [TrinityCore Extractors](https://ci.appveyor.com/project/DDuarte/trinitycore/branch/master/artifacts) for now
* Run all extractors in the WoW directory
* Copy all created folders into server directory (ex: C:\WoWCore\Data)
* Make sure Conf files are updated and point the the correct folders and sql user and databases

### Installing the database
* Extract the sql files into the core sql folder (ex: C:\WoWCore\sql)

### Playing
* Must use [Arctium WoW-Launcher](https://github.com/Arctium/WoW-Launcher/releases)

### Support / General Info
* Check out [CypherCore Discord](https://discord.gg/tCx3JbJ5qQ)
* Check out [TrinityCore Discord](https://discord.com/invite/6MWwZp5M)
* Check out [TrinityCore Wiki](https://trinitycore.info) as a few steps are the same

### Legal
* Blizzard, Battle.net, World of Warcraft, and all associated logos and designs are trademarks or registered trademarks of Blizzard Entertainment.
* All other trademarks are the property of their respective owners. This project is **not** affiliated with Blizzard Entertainment or any of their family of sites.