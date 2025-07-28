namespace ProbabilityCalculator.API.Services
{
    public class FileCalculationLogger : ICalculationLogger
    {
        private const string LogFilePath = "Logs/calculation_log.txt";

        public void Log(OperationType type, double a, double b, double result)
        {
			var line = FormattableString.Invariant(
                 $"{DateTime.UtcNow:u} | {type} | A: {a} B: {b} => {result}");

			Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath)!);
			File.AppendAllLines(LogFilePath, new[] { line });
        }
    }
}