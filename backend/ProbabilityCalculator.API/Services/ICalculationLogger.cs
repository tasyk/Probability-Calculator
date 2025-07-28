namespace ProbabilityCalculator.API.Services
{
    public interface ICalculationLogger
    {        
		void Log(OperationType type, double a, double b, double result);
	}
}