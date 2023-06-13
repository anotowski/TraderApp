# TraderApp
Application made as a test assignment for recruting purpouses.
Application consists of two primary adaptors one is web API project used to retrieve and list data saved in blob and table storage. Second primary adaptor driving is an azure function that gets data from external page via HTTP Client and saves the result of that action to the blob & table storage. Azure function is meant to be ran on Http timer trigger.
## Tech stack
### General
- C# 11
- .Net framework 6.0
### Testing
  - nUnit
  - autofixture
  - nSubstitute

## Architecture overview
Project is fallowing basic principles of clean architecture ( also called onion layered or hexagonal architecture).
In order to spice things up project also implements CQS(Command Query Separation) pattern by authors own implementation.
Project will use some of the tactical Domain Driven Design patterns but is not a fully DDD project. This is an architectual decission that was made and has reflection in the decission log.

### Project structure

![img.png](img.png)
Project consists of 4 main layers:
- Primary adaptors: TraderApp.Api(number 2) & TraderApp.FunctionApp(number 5)
- Application: TraderApp.Application(number 3) - this is the layer that is doing all validation work and communicates with infrastructure layer using domain entities and services.
- Domain: TraderApp.Domain - this layer holds all information about domain. This layer shouldn't depend on any other layer nor any third party tools but other layers can benefit from this layer.
- Infrastructure: TraderApp.Infrastructure(number 6) - This layer is working with Secondary(driven) adaptors, this means it's communicating with external world.

- In this project there is separate folder to put tests for each of the layer(number 7)
- Project also has a shared layer(number 1) that isn't part of any other layer but can be used by any layer. This layer is transcient to all other layers. You can think of it as a separate package that is used to help structure project.
