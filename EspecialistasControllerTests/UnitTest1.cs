using Moq;
using Proyecto.Controllers;
using Proyecto.Models;
using Proyecto.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

public class EspecialistasControllerTests
{
    private readonly Mock<IUnitWork> _unitWorkMock;
    private readonly EspecialistasController _controller;

    public EspecialistasControllerTests()
    {
        // Crear el mock de IUnitWork
        _unitWorkMock = new Mock<IUnitWork>();

        // Pasamos el mock al controlador
        _controller = new EspecialistasController(_unitWorkMock.Object);
    }
    
    //GET
    [Fact]
    public void Create_Get_ReturnsViewWithEspecialista()
    {
        // Actuar
        var result = _controller.Create();

        // Aserciones
        var viewResult = Assert.IsType<ViewResult>(result);  // Verificar que el resultado es una vista
        var model = Assert.IsType<Especialista>(viewResult.Model);  // Verificar que el modelo es de tipo Especialista

        // Verificar que el modelo tiene las propiedades inicializadas
        Assert.True(model.Status);  // Estado por defecto en true
        Assert.Equal(DateTime.Now.Date, model.CreatedAt.Date);  // Fecha actual
    }

    //POST

    [Fact]
    public async Task Create_Post_ValidEspecialista_ReturnsRedirectToAction()
    {
        // Arrange
        var mockUnitWork = new Mock<IUnitWork>();
        var controller = new EspecialistasController(mockUnitWork.Object);

        // Simula TempData
        controller.TempData = new Mock<ITempDataDictionary>().Object;

        var especialista = new Especialista
        {
            //EspecialistaId = 1,
            FirstName = "Juan Perez",
            Status = true
        };

        // Simula que el método AgregarAsync se llama correctamente
        mockUnitWork.Setup(u => u.Especialista.AgregarAsync(It.IsAny<Especialista>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await controller.Create(especialista);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);

        // Verifica que se haya llamado al método GuardarAsync
        mockUnitWork.Verify(u => u.GuardarAsync(), Times.Once);
    }


    [Fact]
    public async Task Create_Post_InvalidModelState_ReturnsViewWithModel()
    {
        // Arrange
        var mockUnitWork = new Mock<IUnitWork>();
        var controller = new EspecialistasController(mockUnitWork.Object);

        var especialista = new Especialista
        {
            FirstName = "Juan Perez",
            Status = true
        };

        // Simula que el ModelState no es válido
        controller.ModelState.AddModelError("FirstName", "Required");

        // Act
        var result = await controller.Create(especialista);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Especialista>(viewResult.Model);
        Assert.Equal(especialista, model); // Verifica que el modelo devuelto es el mismo
    }

}
