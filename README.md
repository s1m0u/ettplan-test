# Ettplan - Arbetstest
## Simon

### What?
This app reads an XML file in the XLIFF format and saves the target for a specific id (42014).
The result is then saved as an XML file.

### Running the project
The app can be run from the console by using
`dotnet run`
from the App project (see Project structure)

Alternativly open the .sln file in the Visual Studio editor and run the project with Etteplan.App as startup project.


#### Project structure
All project follow the format Etteplan.[project name]

| Project name | Description |
| ----------- | ----------- |
| App | Simple Console app to run the implementation and get basic visual feedback. |
| Core | Contains the implementations domain objects and the business logic. The XmlParser is the main business logic for parsing and searching the xml. |
| Test | Contains test for the XmlParser |

