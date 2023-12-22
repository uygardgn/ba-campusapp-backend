using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BACampusApp.WebApi
{
	public class CustomHeaderSwaggerAttribute:IOperationFilter
	{
		//Bu metod Swagger'a istediğimiz bir filterinin eklenmesini sağlamaktadır. Error ve Success mesajlarının seçilen dile bağlı çalışması için "Accept-Language" parametresi Swagger'ın header'ına eklenmiştir.
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.Parameters == null)
				operation.Parameters = new List<OpenApiParameter>();

			operation.Parameters.Add(new OpenApiParameter
			{
				Name = "Accept-Language",
				In = ParameterLocation.Header,
				Required = true,
				Schema = new OpenApiSchema
				{
					Type = "string",
					Default = new OpenApiString("tr-TR")
				}
			});
		}

	}
}
