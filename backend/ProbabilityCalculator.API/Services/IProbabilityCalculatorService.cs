
namespace ProbabilityCalculator.API.Services
{
    public interface IProbabilityCalculatorService
    {
        bool IsValid(double a, double b);
        double Calculate(OperationType type, double a, double b);
    }
}