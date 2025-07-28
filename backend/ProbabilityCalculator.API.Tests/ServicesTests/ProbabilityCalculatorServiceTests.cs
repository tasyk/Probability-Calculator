using ProbabilityCalculator.API.Services;

namespace ProbabilityCalculator.API.Tests.ServicesTests
{
	[TestClass]
	public class ProbabilityCalculatorServiceTests
	{
		private ProbabilityCalculatorService _service = null!;

		[TestInitialize]
		public void Setup()
		{
			_service = new ProbabilityCalculatorService();
		}

		[TestMethod]
		[DataRow(0.0, 0.0, true)]
		[DataRow(0.5, 0.5, true)]
		[DataRow(1.0, 1.0, true)]
		[DataRow(-0.1, 0.5, false)]
		[DataRow(0.5, 1.1, false)]
		[DataRow(-0.1, 1.1, false)]
		public void IsValid_ReturnsExpected(double a, double b, bool expected)
		{
			var result = _service.IsValid(a, b);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void Calculate_CombinedWith_ReturnsProduct()
		{
			var result = _service.Calculate(OperationType.CombinedWith, 0.4, 0.5);
			Assert.AreEqual(0.2, result);
		}

		[TestMethod]
		public void Calculate_Either_ReturnsCombinedProbability()
		{
			var result = _service.Calculate(OperationType.Either, 0.4, 0.5);
			// A + B - A*B = 0.4 + 0.5 - 0.2 = 0.7
			Assert.AreEqual(0.7, result);
		}

		[TestMethod]
		public void Calculate_InvalidOperation_ThrowsArgumentException()
		{
			var invalidOp = (OperationType)999;

			Assert.ThrowsException<ArgumentException>(() =>
			{
				_service.Calculate(invalidOp, 0.4, 0.5);
			});
		}
	}
}
