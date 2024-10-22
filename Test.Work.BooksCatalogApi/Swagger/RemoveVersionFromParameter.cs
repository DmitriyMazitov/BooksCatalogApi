using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Test.Work.BooksCatalogApi.Swagger
{
    /// <summary>
	/// Убрать из параметров API-запросов версию самого API, она заполняется в <see cref="ReplaceVersionWithExactValueInPath"/>
	/// </summary>
	public class RemoveVersionFromParameter : IOperationFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "version");
            if (versionParameter != null)
                operation.Parameters.Remove(versionParameter);
        }
    }
}
