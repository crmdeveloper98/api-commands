using System;
using CommandAPI.Models;
using Shouldly;
using Xunit;

namespace CommandAPI.Tests
{
    public class CommandTests : IDisposable
    {
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "Some platform",
                CommandLine = "Some commandline"                
            };
        }

        [Fact]
        public void CanChangeHowTo()
        {
            // Arrange

            // Act
            testCommand.HowTo = "Execute Unit Tests";

            // Assert
   
            // Use Shouldly
            testCommand.HowTo.ShouldBe("Execute Unit Tests");
        }

        [Fact]
        public void CanChangePlatform()
        {
            // Arrange


            // Act
            testCommand.Platform = "xUnit";

            // Assert
            
            // Use Shouldly
            testCommand.Platform.ShouldBe("xUnit");
        }      

        [Fact]
        public void CanChangeCommandLine()
        {
            // Arrange

            // Act
            testCommand.CommandLine = "dotnet test";

            // Assert
            
            // Use Shouldly
            testCommand.CommandLine.ShouldBe("dotnet test");
        }

        public void Dispose()
        {
            testCommand = null;
        }
    }
}