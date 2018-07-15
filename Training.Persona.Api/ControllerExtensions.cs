namespace Training.Persona.Api
{
    using System;
    using System.Linq;

    using Apollo.NetCore.Core.Exceptions;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Primitives;

    /// <summary>
    /// Extension methods para Controller.
    /// </summary>
    public static class ControllerExtensions
    {
        #region Methods

        /// <summary>
        /// Simula la Validación del accessTokken obteniéndolo del header del request.
        /// </summary>
        /// <param name="controller">El controller.</param>
        public static void CheckAccessToken(this Controller controller)
        {
            controller.Request.Headers.TryGetValue("Access-Token", out StringValues accessToken);

            if (accessToken.Any())
            {
                switch (accessToken[0])
                {
                    case "token_valido":
                        // Ok.
                        break;

                    case "token_expirado":
                        // Expirado.
                        throw new UnauthorizedException().AddErrorCode(ExErrorCodeCore.ErrAuthAccesstokenExpired);

                    default:
                        // Inválido.
                        throw new UnauthorizedException().AddErrorCode(ExErrorCodeCore.ErrAuthAccesstokenInvalid);
                }
            }
            else
            {
                // Inválido.
                throw new UnauthorizedException().AddErrorCode(ExErrorCodeCore.ErrAuthAccesstokenInvalid);
            }
        }

        #endregion
    }
}