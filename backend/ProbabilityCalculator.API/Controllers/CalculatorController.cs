using Microsoft.AspNetCore.Mvc;
using ProbabilityCalculator.API.Entities;
using ProbabilityCalculator.API.Services;
using System.ComponentModel.DataAnnotations;

namespace ProbabilityCalculator.Api.Controllers
{
	/// <summary>
	/// Controller for calcuation probability
	/// </summary>
	[ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly IProbabilityCalculatorService _calculator;
        private readonly ICalculationLogger _logger;

        public CalculatorController(IProbabilityCalculatorService calculator, ICalculationLogger logger)
        {
            _calculator = calculator;
            _logger = logger;
        }

        [HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(double))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult Calculate([FromBody] ProbabilityRequest request)
        {
            if (!_calculator.IsValid(request.A, request.B))
                return BadRequest("Probabilities must be between 0 and 1.");

            try
            {
                var result = _calculator.Calculate(request.Type, request.A, request.B);
                _logger.Log(request.Type, request.A, request.B, result);
                return Ok( result );
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid operation type.");
            }
        }
    }
}