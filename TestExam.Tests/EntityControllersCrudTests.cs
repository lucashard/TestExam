using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TestExam.Controllers;
using TestExam.Domain;

namespace TestExam.Tests;

public class DireccionControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenDireccionExists()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetDireccionByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Direccion { Id = 1, Nombre = "Calle", Altura = 123 });
        var controller = new DireccionController(mediator.Object);

        var result = await controller.Get(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Post_ReturnsCreated_WhenDireccionIsCreated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<CreateDireccionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var controller = new DireccionController(mediator.Object);

        var result = await controller.Post(new CreateDireccionCommand("Calle", 123));

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenDireccionIsUpdated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateDireccionCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var controller = new DireccionController(mediator.Object);

        var result = await controller.Put(1, new UpdateDireccionCommand(0, "Nueva", 10));

        Assert.IsType<NoContentResult>(result);
    }
}

public class ClienteControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenClienteExists()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetClienteByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Cliente { Id = 1, Nombre = "Ana", Apellido = "Diaz", Documento = "1", Email = "a@a.com", Telefono = "123", DireccionId = 1 });
        var controller = new ClienteController(mediator.Object);

        var result = await controller.Get(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Post_ReturnsCreated_WhenClienteIsCreated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<CreateClienteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var controller = new ClienteController(mediator.Object);

        var result = await controller.Post(new CreateClienteCommand("Ana", "Diaz", "1", "a@a.com", "123", 1));

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenClienteIsUpdated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateClienteCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var controller = new ClienteController(mediator.Object);

        var result = await controller.Put(1, new UpdateClienteCommand(0, "Ana", "Diaz", "1", "a@a.com", "123", 1));

        Assert.IsType<NoContentResult>(result);
    }
}

public class EtapasControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenEtapaExists()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetEtapasByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Etapas { Id = 1, Nombre = "Mezcla" });
        var controller = new EtapasController(mediator.Object);

        var result = await controller.Get(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Post_ReturnsCreated_WhenEtapaIsCreated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<CreateEtapasCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var controller = new EtapasController(mediator.Object);

        var result = await controller.Post(new CreateEtapasCommand("Mezcla"));

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenEtapaIsUpdated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateEtapasCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var controller = new EtapasController(mediator.Object);

        var result = await controller.Put(1, new UpdateEtapasCommand(0, "Coccion"));

        Assert.IsType<NoContentResult>(result);
    }
}

public class OperariosControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenOperarioExists()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetOperariosByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Operarios { Id = 1, Nombre = "Juan" });
        var controller = new OperariosController(mediator.Object);

        var result = await controller.Get(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Post_ReturnsCreated_WhenOperarioIsCreated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<CreateOperariosCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var controller = new OperariosController(mediator.Object);

        var result = await controller.Post(new CreateOperariosCommand("Juan"));

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenOperarioIsUpdated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateOperariosCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var controller = new OperariosController(mediator.Object);

        var result = await controller.Put(1, new UpdateOperariosCommand(0, "Pedro"));

        Assert.IsType<NoContentResult>(result);
    }
}

public class OperarioEtapaControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenOperarioEtapaExists()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetOperarioEtapaByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new OperarioEtapa { Id = 1, EtapaId = 1, OperarioId = 1, Duracion = 3 });
        var controller = new OperarioEtapaController(mediator.Object);

        var result = await controller.Get(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Post_ReturnsCreated_WhenOperarioEtapaIsCreated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<CreateOperarioEtapaCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var controller = new OperarioEtapaController(mediator.Object);

        var result = await controller.Post(new CreateOperarioEtapaCommand(1, 1, 2));

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenOperarioEtapaIsUpdated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateOperarioEtapaCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var controller = new OperarioEtapaController(mediator.Object);

        var result = await controller.Put(1, new UpdateOperarioEtapaCommand(0, 1, 1, 5));

        Assert.IsType<NoContentResult>(result);
    }
}

public class FacturaControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WhenFacturaExists()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<GetFacturaByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Factura { Id = 1, ClienteId = 1 });
        var controller = new FacturaController(mediator.Object);

        var result = await controller.Get(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Post_ReturnsCreated_WhenFacturaIsCreated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<CreateFacturaCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var controller = new FacturaController(mediator.Object);

        var result = await controller.Post(new CreateFacturaCommand(1, [1, 2]));

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Put_ReturnsNoContent_WhenFacturaIsUpdated()
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send(It.IsAny<UpdateFacturaCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var controller = new FacturaController(mediator.Object);

        var result = await controller.Put(1, new UpdateFacturaCommand(0, 2, [3]));

        Assert.IsType<NoContentResult>(result);
    }
}

