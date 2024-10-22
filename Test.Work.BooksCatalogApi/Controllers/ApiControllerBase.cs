using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Work.BooksCatalogApi.Contracts.Services;

namespace Test.Work.BooksCatalogApi.Controllers
{
    /// <summary>
	/// Базовый API-контроллер
	/// </summary>
	[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ProblemDetailsResponse))]
    public class ApiControllerBase : ControllerBase
    {
    }
}
