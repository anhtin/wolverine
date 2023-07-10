// <auto-generated/>
#pragma warning disable
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using Wolverine.Http;
using Wolverine.Marten.Publishing;
using Wolverine.Runtime;

namespace Internal.Generated.WolverineHandlers
{
    // START: POST_reservation
    public class POST_reservation : Wolverine.Http.HttpHandler
    {
        private readonly Wolverine.Http.WolverineHttpOptions _wolverineHttpOptions;
        private readonly Wolverine.Runtime.IWolverineRuntime _wolverineRuntime;
        private readonly Wolverine.Marten.Publishing.OutboxedSessionFactory _outboxedSessionFactory;

        public POST_reservation(Wolverine.Http.WolverineHttpOptions wolverineHttpOptions, Wolverine.Runtime.IWolverineRuntime wolverineRuntime, Wolverine.Marten.Publishing.OutboxedSessionFactory outboxedSessionFactory) : base(wolverineHttpOptions)
        {
            _wolverineHttpOptions = wolverineHttpOptions;
            _wolverineRuntime = wolverineRuntime;
            _outboxedSessionFactory = outboxedSessionFactory;
        }



        public override async System.Threading.Tasks.Task Handle(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            var messageContext = new Wolverine.Runtime.MessageContext(_wolverineRuntime);
            await using var documentSession = _outboxedSessionFactory.OpenSession(messageContext);
            var (start, jsonContinue) = await ReadJsonAsync<WolverineWebApi.StartReservation>(httpContext);
            if (jsonContinue == Wolverine.HandlerContinuation.Stop) return;
            (var reservationBooked, var reservation, var reservationTimeout) = WolverineWebApi.ReservationEndpoint.Post(start);
            
            // Register the document operation with the current session
            documentSession.Insert(reservation);
            
            // Outgoing, cascaded message
            await messageContext.EnqueueCascadingAsync(reservationTimeout).ConfigureAwait(false);

            
            // Commit any outstanding Marten changes
            await documentSession.SaveChangesAsync(httpContext.RequestAborted).ConfigureAwait(false);

            await WriteJsonAsync(httpContext, reservationBooked);
        }

    }

    // END: POST_reservation
    
    
}
