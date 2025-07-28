//using Microsoft.OpenApi.Models;
using ProbabilityCalculator.API.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProbabilityCalculator.API.Entities
{	public class ProbabilityRequest
	{
		/// <summary>
		/// First probability value (e.g. 0.5)
		/// </summary>
		[Required]
		[Range(0, 1)]
		[DefaultValue(0d)]
		public double A { get; set; }

		/// <summary>
		/// Second probability value (e.g. 0.3)
		/// </summary>
		[Required]
		[Range(0, 1)]
		[DefaultValue(1d)]
		public double B { get; set; }

		/// <summary>
		/// Operation type (e.g. CombinedWith)
		/// </summary>
		[Required]
		[JsonConverter(typeof(JsonStringEnumConverter))]
		[DefaultValue(OperationType.CombinedWith)]
		public OperationType Type { get; set; }
	}
}
