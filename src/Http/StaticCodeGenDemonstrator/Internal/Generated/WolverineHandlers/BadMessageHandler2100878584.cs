// <auto-generated/>

using System.Diagnostics;

#pragma warning disable

namespace Internal.Generated.WolverineHandlers
{
    // START: BadMessageHandler2100878584
    public class BadMessageHandler2100878584 : Wolverine.Runtime.Handlers.MessageHandler
    {
        public BadMessageHandler2100878584()
        {
            Debug.WriteLine("Here");
        }

        public override System.Threading.Tasks.Task HandleAsync(Wolverine.Runtime.MessageContext context, System.Threading.CancellationToken cancellation)
        {
            // The actual message body
            var badMessage = (BadMessage)context.Envelope.Message;

            
            // The actual message execution
            BadMessageHandler.Handle(badMessage);

            return System.Threading.Tasks.Task.CompletedTask;
        }

    }

    // END: BadMessageHandler2100878584
    
    
}
