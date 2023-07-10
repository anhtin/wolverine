// <auto-generated/>
#pragma warning disable
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using Wolverine.Http;

namespace Internal.Generated.WolverineHandlers
{
    // START: POST_users_sign_up
    public class POST_users_sign_up : Wolverine.Http.HttpHandler
    {
        private readonly Wolverine.Http.WolverineHttpOptions _wolverineHttpOptions;

        public POST_users_sign_up(Wolverine.Http.WolverineHttpOptions wolverineHttpOptions) : base(wolverineHttpOptions)
        {
            _wolverineHttpOptions = wolverineHttpOptions;
        }



        public override async System.Threading.Tasks.Task Handle(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var (request, jsonContinue) = await ReadJsonAsync<WolverineWebApi.SignUpRequest>(httpContext);
            if (jsonContinue == Wolverine.HandlerContinuation.Stop) return;
            var result = WolverineWebApi.SignupEndpoint.SignUp(request);
            await result.ExecuteAsync(httpContext).ConfigureAwait(false);
        }

    }

    // END: POST_users_sign_up
    
    
}
