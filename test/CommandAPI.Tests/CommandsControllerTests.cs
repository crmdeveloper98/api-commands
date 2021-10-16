using System;
using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests : IDisposable
    {
        Mock<ICommandAPIRepository> mockRepository;
        CommandsProfile realProfile;
        MapperConfiguration configuration;
        IMapper mapper;

        public CommandsControllerTests()
        {
            mockRepository = new Mock<ICommandAPIRepository>();            
            realProfile = new CommandsProfile();
            configuration = new MapperConfiguration(config => config.AddProfile(realProfile));
            mapper = new Mapper(configuration); 
        }

        public void Dispose()
        {
           
            mockRepository = null;
            realProfile = null;
            configuration = null;;
            mapper = null;
        }

        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetAllCommands()).Returns(GetCommand(0));                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.GetAllCommands();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetAllCommands_ReturndOneItem_WhenDBHasOneResource()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetAllCommands()).Returns(GetCommand(1));                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.GetAllCommands();      

            // Assert
            var okResult = result.Result as OkObjectResult;
            var commands = okResult.Value as List<CommandReadDto>;      

            Assert.Single(commands);
        }

        [Fact]
        public void GetAllCommands_Returns200OK_WhenDBHasOneResource()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetAllCommands()).Returns(GetCommand(1));                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.GetAllCommands();    

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);  

        }

        [Fact]
        public void GetAllCommands_ReturnsCorrectType_WhenDBHasOneResource()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetAllCommands()).Returns(GetCommand(1));                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.GetAllCommands();    

            // Assert
            Assert.IsType<ActionResult<IEnumerable<CommandReadDto>>>(result);          
        }

        [Fact]
        public void GetCommandByID_Returns404NotFound_WhenNonExistentIDIsProvided()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(0)).Returns(() => null);                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);   

            // Assert
            Assert.IsType<NotFoundResult>(result.Result); 

        }

        [Fact]
        public void GetCommandByID_Returns200OK_WhenValidIDIsProvided()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);   

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);             
        }

        [Fact]
        public void GetCommandByID_ReturnsCorrectObjectType_WhenValidIDIsProvided()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.GetCommandById(1);   

            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);   
        }

        [Fact]
        public void CreateCommand_ReturnsCorrectResourceType_WhenValidObjectIsSubmitted()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.CreateCommand(new CommandCreateDto { });

            // Assert
            Assert.IsType<ActionResult<CommandReadDto>>(result);         
        }

        [Fact]
        public void CreateCommand_Returns201Created_WhenValidObjectIsSubmitted()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });                         
            var controller = new CommandsController( mockRepository.Object, mapper);

            // Act
            var result = controller.CreateCommand(new CommandCreateDto { });

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);            
        }

        [Fact]
        public void UpdateCommand_Returns204NoContent_WhenValidObjectIsSubmitted()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });                         
            var controller = new CommandsController( mockRepository.Object, mapper);     

            // Act
            var result = controller.UpdateCommand(1, new CommandUpdateDto { });

            // Assert
            Assert.IsType<NoContentResult>(result);       
        }

        [Fact]
        public void UpdateCommand_Returns404NotFound_WhenNonExistentResourceIDIsSubmitted()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(0)).Returns(() => null);                         
            var controller = new CommandsController( mockRepository.Object, mapper);     

            // Act
            var result = controller.UpdateCommand(0, new CommandUpdateDto { }); 

            // Assert
            Assert.IsType<NotFoundResult>(result);           
        }

        [Fact]
        public void PartialCommandUpdate_Returns404NotFound_WhenNonExistentResourceIDIsSubmitted()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(0)).Returns(() => null);                         
            var controller = new CommandsController( mockRepository.Object, mapper); 

            var result = controller.PartialCommandUpdate(0, 
                new Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<CommandUpdateDto> { });

            // Assert
            Assert.IsType<NotFoundResult>(result);    

        }

        [Fact]
        public void DeleteCommand_Returns204NoContent_WhenValidResourceIDisSubmitted()
        {
            // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(1)).Returns(new Command
            {
                Id = 1,
                HowTo = "mock",
                Platform = "Mock",
                CommandLine = "Mock"
            });                         
            var controller = new CommandsController( mockRepository.Object, mapper);  

            // Act
            var result = controller.DeleteCommand(1);

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        void DeleteCommand_Returns404NotFound_WhenNonExistentResourceIDIsSubmitted()
        {
           // Arrange
            mockRepository.Setup(repository => repository.GetCommandById(0)).Returns(() => null);                         
            var controller = new CommandsController( mockRepository.Object, mapper);  

            // Act
            var result = controller.DeleteCommand(0);

            // Assert
            Assert.IsType<NotFoundResult>(result);            
        }

        private List<Command> GetCommand(int n)
        {
            var commands = new List<Command>();
            if (n > 0) {
                commands.Add(new Command
                {
                    Id = 0,
                    HowTo = "How to generate a migration",
                    CommandLine = "dotnet ef migrations add <Name of Migration>",
                    Platform = ".Net Core EF"
                });
            }

            return commands;
        }
    }
}