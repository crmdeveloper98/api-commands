using System;
using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Controllers;
using CommandAPI.Data;
using CommandAPI.Models;
using CommandAPI.Profiles;
using Moq;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandsControllerTests
    {
        [Fact]
        public void GetCommandItems_ReturnsZeroItems_WhenDBIsEmpty()
        {
            // Arrange

            var mockRepository = new Mock<ICommandAPIRepository>();
            mockRepository.Setup(repository => repository.GetAllCommands()).Returns(GetCommand(0));

            var realProfile = new CommandsProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(realProfile));
            IMapper mapper = new Mapper(configuration);
                        
            var controller = new CommandsController( mockRepository.Object, mapper);
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