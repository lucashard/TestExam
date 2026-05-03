using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestExam.Controllers;

namespace TestExam.Tests;

public class MateriaPrimaControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsOk_WithAllItems_WhenNoFiltersAreSent()
    {
        var expected = new List<MateriaPrima>
        {
            new() { Id = 1, Nombre = "Harina", Descripcion = "Harina de trigo", Cantidad = 100 },
            new() { Id = 2, Nombre = "Azucar", Descripcion = "Azucar refinada", Cantidad = 200 }
        };

        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.IsAny<GetAllMateriaPrimaQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var controller = new MateriaPrimaController(mediator.Object);

        var actionResult = await controller.GetAll(null, null, null);

        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var payload = Assert.IsAssignableFrom<IEnumerable<MateriaPrima>>(okResult.Value);
        Assert.Equal(2, payload.Count());
    }

    [Fact]
    public async Task GetAll_PropagatesAllFilters_ToMediatorQuery()
    {
        const int id = 7;
        const string nombre = "hari";
        const string descripcion = "trigo";

        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.IsAny<GetAllMateriaPrimaQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<MateriaPrima>());

        var controller = new MateriaPrimaController(mediator.Object);

        _ = await controller.GetAll(id, nombre, descripcion);

        mediator.Verify(
            m => m.Send(
                It.Is<GetAllMateriaPrimaQuery>(q =>
                    q.Id == id &&
                    q.Nombre == nombre &&
                    q.Descripcion == descripcion),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenEntityExists()
    {
        const int id = 2;
        var expected = new MateriaPrima { Id = id, Nombre = "Azucar", Descripcion = "Azucar refinada", Cantidad = 200 };

        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.Is<GetMateriaPrimaByIdQuery>(q => q.Id == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var controller = new MateriaPrimaController(mediator.Object);

        var actionResult = await controller.Get(id);

        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        var payload = Assert.IsType<MateriaPrima>(okResult.Value);
        Assert.Equal(id, payload.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        const int id = 404;

        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.Is<GetMateriaPrimaByIdQuery>(q => q.Id == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MateriaPrima?)null);

        var controller = new MateriaPrimaController(mediator.Object);

        var actionResult = await controller.Get(id);

        Assert.IsType<NotFoundResult>(actionResult);
    }

    [Fact]
    public async Task Post_ReturnsCreatedAtAction_WithCreatedId()
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.IsAny<CreateMateriaPrimaCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(10);

        var controller = new MateriaPrimaController(mediator.Object);

        var result = await controller.Post(new CreateMateriaPrimaCommand("Sal", "Fina", 20));

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("Get", created.ActionName);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenUpdated()
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.IsAny<UpdateMateriaPrimaCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var controller = new MateriaPrimaController(mediator.Object);

        var result = await controller.Put(5, new UpdateMateriaPrimaCommand(0, "Nombre", "Desc", 10));

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNotFound_WhenUpdateFails()
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(m => m.Send(It.IsAny<UpdateMateriaPrimaCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var controller = new MateriaPrimaController(mediator.Object);

        var result = await controller.Put(5, new UpdateMateriaPrimaCommand(0, "Nombre", "Desc", 10));

        Assert.IsType<NotFoundResult>(result);
    }
}
