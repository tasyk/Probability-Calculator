using Microsoft.AspNetCore.Mvc;
using Moq;
using ProbabilityCalculator.Api.Controllers;
using ProbabilityCalculator.API.Entities;
using ProbabilityCalculator.API.Services;

namespace ProbabilityCalculator.API.Tests.ControllerTests
{
	[TestClass]
	public class CalculatorControllerTests
	{
		private Mock<IProbabilityCalculatorService> _calculatorMock = null!;
		private Mock<ICalculationLogger> _loggerMock = null!;
		private CalculatorController _controller = null!;

		[TestInitialize]
		public void Setup()
		{
			_calculatorMock = new Mock<IProbabilityCalculatorService>();
			_loggerMock = new Mock<ICalculationLogger>();
			_controller = new CalculatorController(_calculatorMock.Object, _loggerMock.Object);
		}

		[TestMethod]
		public void Calculate_ValidInput_ReturnsOkResult()
		{
			// Arrange
			var request = new ProbabilityRequest { A = 0.5, B = 0.4, Type = OperationType.CombinedWith };
			_calculatorMock.Setup(c => c.IsValid(0.5, 0.4)).Returns(true);
			_calculatorMock.Setup(c => c.Calculate(OperationType.CombinedWith, 0.5, 0.4)).Returns(0.2);

			// Act
			var result = _controller.Calculate(request);

			// Assert
			var okResult = result as OkObjectResult;
			Assert.IsNotNull(okResult);
			Assert.AreEqual(200, okResult.StatusCode);
			Assert.AreEqual(0.2, okResult.Value);
			_loggerMock.Verify(l => l.Log(OperationType.CombinedWith, 0.5, 0.4, 0.2), Times.Once);
		}

		[TestMethod]
		public void Calculate_InvalidProbabilities_ReturnsBadRequest()
		{
			// Arrange
			var request = new ProbabilityRequest { A = -0.1, B = 1.2, Type = OperationType.CombinedWith };
			_calculatorMock.Setup(c => c.IsValid(-0.1, 1.2)).Returns(false);

			// Act
			var result = _controller.Calculate(request);

			// Assert
			var badRequest = result as BadRequestObjectResult;
			Assert.IsNotNull(badRequest);
			Assert.AreEqual(400, badRequest.StatusCode);
			Assert.AreEqual("Probabilities must be between 0 and 1.", badRequest.Value);
		}

		[TestMethod]
		public void Calculate_InvalidType_ThrowsArgumentException_ReturnsBadRequest()
		{
			// Arrange
			var request = new ProbabilityRequest { A = 0.4, B = 0.6, Type = OperationType.Either };
			_calculatorMock.Setup(c => c.IsValid(0.4, 0.6)).Returns(true);
			_calculatorMock.Setup(c => c.Calculate(OperationType.Either, 0.4, 0.6)).Throws<ArgumentException>();

			// Act
			var result = _controller.Calculate(request);

			// Assert
			var badRequest = result as BadRequestObjectResult;
			Assert.IsNotNull(badRequest);
			Assert.AreEqual(400, badRequest.StatusCode);
			Assert.AreEqual("Invalid operation type.", badRequest.Value);
		}
	}
}
