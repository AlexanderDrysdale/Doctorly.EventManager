using Doctorly.EventManager.Api.Application.UseCases;
using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;
using Moq;
using Xunit;

public class GetEventByIdHandlerTests
{
    [Fact]
    public void Handle_ReturnsEventDto_WhenEventExists()
    {
        // Arrange
        var mockRepo = new Mock<IEventRepository>();
        var ev = new Event { Id = 1, Title = "Test Event", Description = "Desc" };
        mockRepo.Setup(r => r.GetById(1)).Returns(ev);

        var handler = new GetEventByIdHandler(mockRepo.Object);

        // Act
        var result = handler.Handle(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Event", result.Title);
    }

    [Fact]
    public void Handle_ReturnsNull_WhenEventDoesNotExist()
    {
        var mockRepo = new Mock<IEventRepository>();
        mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((Event?)null);

        var handler = new GetEventByIdHandler(mockRepo.Object);

        var result = handler.Handle(99);

        Assert.Null(result);
    }
}
