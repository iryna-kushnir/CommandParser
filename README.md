# CommandParser
Command line arguments parser - test task for Kottans

**To start using CommandParser:**

*1. Install .Net framework*

*2. Build CommandParser project:*

1) Using Visual Studio/Mono Develop/Other IDE:
- Open solution file CommandParser.sln at your IDE
- Right click on the solution/project -> Build

2) Using MSBuild:
- From command prompt open folder containing CommandParser.sln
- Run msbuild command (you can use parameters /t:Rebuild /p:Configuration=Release /p:Platform="any cpu"). You may need to specify path to msbuild.exe.

*3. From command prompt open folder <path to solution folder>\CommandParser\bin\Release*

*4. Run CommandParser.exe. You will see help with the list of supported commands.*