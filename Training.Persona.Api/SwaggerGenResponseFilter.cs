namespace Training.Persona.Api
{
    using System.Collections.Generic;
    using System.Text;

    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// Filtro de operaciones de api que permite modificar la documentación de Swagger.
    /// </summary>
    public class SwaggerGenResponseFilter : IOperationFilter
    {
        #region Methods

        /// <summary>
        /// Aplica las modificaciones en la documentación de Swagger a la operación especificada.
        /// </summary>
        /// <param name="operation">La operación a la que aplica el filtro.</param>
        /// <param name="context">Contexto de la operación.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            // 400 BadRequest.
            Response response400 = new Response();
            string responseKey400 = "400 BadRequest";
            StringBuilder description400Builder = new StringBuilder();

            description400Builder.Append("Requested invalid or incomplete (See ValidationErrors). ErrorCode: **ERR.VALIDATION**.");

            // Agrega o crea un response.
            if (operation.Responses.ContainsKey(responseKey400))
            {
                description400Builder
                    .AppendLine()
                    .AppendLine().Append((string)operation.Responses[responseKey400].Description);

                response400.Description = description400Builder.ToString();
                operation.Responses[responseKey400] = response400;
            }
            else
            {
                response400.Description = description400Builder.ToString(); 
                operation.Responses.Add(new KeyValuePair<string, Response>(responseKey400, response400));
            }

            // 401 Unauthorized.
            Response response401 = new Response();
            string responseKey401 = "401 Unauthorized";
            StringBuilder description401Builder = new StringBuilder();
            if (this.IsAuthOperation(context))
            {
                description401Builder.Append("* Missing or invalid ApiKey. ErrorCode: **ERR.AUTH.INVALIDAPIKEY**.")
                    .AppendLine()
                    .Append("* Invalid credentials. ErrorCode: **ERR.AUTH.INVALIDCREDENTIALS**.")
                    .AppendLine();
            }
            else
            {
                description401Builder
                    .Append("* AccessToken invalid. ErrorCode: **ERR.AUTH.ACCESSTOKEN.INVALID**.")
                    .AppendLine()
                    .Append("* AccessToken expired.ErrorCode: **ERR.AUTH.ACCESSTOKEN.EXPIRED**.");
            }

            // Agrega o crea un response.
            if (!ShowGeneric401(context))
            {
                if (operation.Responses.ContainsKey(responseKey401))
                {
                    description401Builder
                        .AppendLine()
                        .AppendLine().Append((string)operation.Responses[responseKey401].Description);

                    response401.Description = description401Builder.ToString();
                    operation.Responses[responseKey401] = response401;
                }
                else
                {
                    response401.Description = description401Builder.ToString();
                    operation.Responses.Add(new KeyValuePair<string, Response>(responseKey401, response401));
                }
            }

            // 500 InternalServerError.
            Response response500 = new Response();
            string responseKey500 = "500 InternalServerError";
            StringBuilder description500Builder = new StringBuilder();
            description500Builder.Append("Server encountered and internal error while attempting to process the request. The response body will include a unique identifier (errUniqueId) that we can use to help locate the problem.");

            // Agrega o crea un response.
            if (operation.Responses.ContainsKey(responseKey500))
            {
                description500Builder
                    .AppendLine()
                    .AppendLine().Append((string)operation.Responses[responseKey500].Description);

                response500.Description = description500Builder.ToString();
                operation.Responses[responseKey500] = response500;
            }
            else
            {
                response500.Description = description500Builder.ToString();
                operation.Responses.Add(new KeyValuePair<string, Response>(responseKey500, response500));
            }
        }

        /// <summary>
        /// Determina si se deben agregar los response messages genéricos para el 401.
        /// </summary>
        /// <param name="context">Contexto de la operación.</param>
        /// <returns>True, si se deben mostrar. De lo contrario, false.</returns>
        private static bool ShowGeneric401(OperationFilterContext context)
        {
            // NOTE: Las operaciones passwordresetrequest y resetpassword no requieren autenticación,
            // con lo cual no deben mostrar el 401 genérigo.
            return context.ApiDescription.RelativePath.Contains("passwordresetrequest")
                   || context.ApiDescription.RelativePath.Contains("resetpassword");
        }

        /// <summary>
        /// Determina si es una operación de autenticación ya que estas requieren algunos responses
        /// distintos a las demás operaciones.
        /// </summary>
        /// <param name="context">Contexto de la operación.</param>
        /// <returns>True, si es una operación de autenticación. False, otras operaciones.</returns>
        private bool IsAuthOperation(OperationFilterContext context)
        {
            bool ret = context.ApiDescription.RelativePath == "auth/accesstoken";

            return ret;
        }

        #endregion
    }
}