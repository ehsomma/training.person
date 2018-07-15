namespace Training.Persona.Api.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;

    using Training.Persona.Entities;

    /// <summary>
    /// Gets an AccessToken to authenticate the user.
    /// </summary>
    public class AccessTokenController : Controller
    {
        /// <summary>
        /// Gets an AccessToken to authenticate the user.
        /// </summary>
        /// <param name="accessTokenRequest">A request for an AccessToken in order to be authenticated in our system.</param>
        /// <returns>AccessToken to authenticate the user.</returns>
        /// <response code="401 Unauthorized">* Cuenta de Persona inactiva. ErrorCode: **ERR.AUTH.INACTIVEACCOUNT**.</response>
        [HttpPost]
        [Route("auth/accesstoken")]
        public AccessToken Post([FromBody] AccessTokenRequest accessTokenRequest)
        {
            throw new NotImplementedException();
        }
    }
}
