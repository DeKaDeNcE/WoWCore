## WoWCore

[WoWCore](https://github.com/DeKaDeNcE/WoWCore) is a open source fork of [CypherCore](https://github.com/CypherCore/CypherCore).

[CypherCore](https://github.com/CypherCore/CypherCore) is a C# open source port of [TrinityCore](https://github.com/TrinityCore/TrinityCore).

The current supported game version is: 10.1.0.50000

### Cloning the repo

To clone the repo you can use this command:

`git clone https://github.com/DeKaDeNcE/WoWCore.git`

### Pulling changes from the repo

Very often we force push to the master branch, because we always rebase it on CypherCore and to keep Git history extremely clean with few commits so that one day they can be merged into upstream CypherCore, so for that reason it's best if you want to pull latest changes using this command:

`git fetch && git reset origin/master --hard`

⚠️ _Just using Pull or Fetch won't work correctly because of the way we force push changes to the repo._ ⚠️

### Compiling the code

Windows: `dotnet build --runtime win-x64 -p:ImportByWildcardBeforeSolution=false`

Linux: `dotnet build --runtime linux-x64 -p:ImportByWildcardBeforeSolution=false`

Mac: `dotnet build --runtime osx-x64 -p:ImportByWildcardBeforeSolution=false`

### Prerequisites
* [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* [MySQL 8.0+](https://dev.mysql.com/downloads/mysql/) or [MariaDB 10.6+](https://mariadb.org/download/)
* Optional: [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/), [Visual Studio Code](https://code.visualstudio.com/download) or [JetBrains Rider](https://www.jetbrains.com/rider/download/#section=windows)

### Server Setup

* You can download already extracted data from [Mega](https://mega.nz/folder/67gBADBR#LvquQabcwaIwPOkP4WQDNA) or [Arctium](https://tc.arctium.io/master/)
* Or you can use [TrinityCore Extractors](https://ci.appveyor.com/project/DDuarte/trinitycore/branch/master/artifacts) for now
* Run all extractors in the WoW directory
* Copy all created folders into server directory (ex: `C:\WoWCore\Data`)
* Make sure your config files are updated and point to the correct folders and you use the correct sql credentials

### Installing the database
* Extract the 7-zipped sql files into the core sql folder (ex: `C:\WoWCore\sql\base`)

### Playing
* You must use [Arctium WoW-Launcher](https://github.com/Arctium/WoW-Launcher/releases) to launch the game and you need to change in `World of Warcraft\_retail_\WTF\Config.wtf` to what server to connect by changing `SET portal "Server IP"` (ex: `SET portal "127.0.0.1"`) 
* The default GM Account is already created and you can login with: Email `GM@GM` Password `admin`
* To create a new account use this command in WorldServer: `bnetaccount create Player@Email YourPassword` then you can login with the email and password you just entered

### Support / General Info
* Check out [CypherCore Discord](https://discord.gg/tCx3JbJ5qQ)
* Check out [TrinityCore Discord](https://discord.com/invite/6MWwZp5M)
* Check out [TrinityCore Wiki](https://trinitycore.info) as a few steps are the same

### Legal
* Blizzard, Battle.net, World of Warcraft, and all associated logos and designs are trademarks or registered trademarks of Blizzard Entertainment.
* All other trademarks are the property of their respective owners. This project is **not** affiliated with Blizzard Entertainment or any of their family of sites.