# TraderApp
Application made as a test assignment for recruting purpouses.
Application consists of two parts connectors one is web API project used to retrieve and list data saved in blob and table storage. Second one is a azure function that gets data from external page via HTTP Client and saves the result of that action to the blob & table storage. Azure function is meant to be ran on Http timer trigger.
## Tech stack
### General
- C# 11
- .Net framework 7.0
### Testing
  - xunit
  - autofixture
  - nSubstitute

## Architecture overview
Project is fallowing basic principles of clean architecture ( also called onion layered or hexagonal architecture).
In order to spice things up project also implements CQS(Command Query Separation) pattern by authors own implementation.
### Project structure
