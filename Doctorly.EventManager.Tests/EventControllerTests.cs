using Doctorly.EventManager.Api.Application.Dtos;
using Doctorly.EventManager.Api.Application.UseCases;
using Doctorly.EventManager.Api.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class EventControllerTests
{
    [Fact]
    public void GetEventById_ReturnsNotFound_WhenEventMissing()
    {
        var mockHandler = new Mock<GetEventByIdHandler>(null);
        mockHandler.Setup(h => h.Handle(It.IsAny<int>())).Returns((EventDto?)null);

        var controller = new EventController(
            null!, mockHandler.Object, null!, null!, null!);

        var result = controller.GetEventById(99);

        Assert.IsType<NotFoundResult>(result);
    }
}
