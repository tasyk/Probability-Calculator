namespace ProbabilityCalculator.API.Services
{
	/// <summary>
	/// Operation type for probability calculation.
	/// </summary>
	public enum OperationType
	{
		Undefined = 0,
		/// <summary>
		/// Logical AND operation.
		/// </summary>
		CombinedWith = 1,

		/// <summary>
		/// Logical OR operation.
		/// </summary>
		Either = 2
	}
}
