using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Engineering_Project.Models.Domian
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {


        /// <summary>
        /// Adds an authorization header to the given operation in Swagger.
        /// </summary>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            IList<FilterDescriptor> filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            bool isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter)
                .Any(filter => filter is AuthorizeFilter);
            bool allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter)
                .Any(filter => filter is IAllowAnonymousFilter);
            if (isAuthorized && !allowAnonymous)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<IParameter>();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "Authorization",
                    In = "header",
                    Description = "access token",
                    Required = true,
                    Type = "string"
                });
            }

            // Policy names map to scopes
            IEnumerable<string> controllerScopes = context.ApiDescription.ControllerAttributes()
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy);

            IEnumerable<string> actionScopes = context.ApiDescription.ActionAttributes()
                .OfType<AuthorizeAttribute>()
                .Select(attr => attr.Policy);

            IEnumerable<string> requiredScopes = controllerScopes.Union(actionScopes).Distinct();

            if (requiredScopes.Any())
            {
                operation.Responses.Add("401", new Response {Description = "Unauthorized"});
                operation.Responses.Add("403", new Response {Description = "Forbidden"});

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                operation.Security.Add(new Dictionary<string, IEnumerable<string>>
                {
                    {"bower", requiredScopes}
                });
            }
        }
    }
}