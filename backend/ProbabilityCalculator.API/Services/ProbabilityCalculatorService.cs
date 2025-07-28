namespace ProbabilityCalculator.API.Services
{
    public class ProbabilityCalculatorService : IProbabilityCalculatorService
    {
        public bool IsValid(double a, double b)
        {
            return a >= 0 && a <= 1 && b >= 0 && b <= 1;
        }

		public double Calculate(OperationType type, double a, double b)
		{
			return type switch
			{
				OperationType.CombinedWith => a * b,
				OperationType.Either => a + b - a * b,
				_ => throw new ArgumentException("Invalid operation type.")
			};
		}
	}
}