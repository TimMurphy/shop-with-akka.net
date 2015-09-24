using Akka.Actor;
using Anotar.Serilog;
using ConsoleApplication.Messages;

namespace ConsoleApplication.Actors
{
    internal class ProductsActor : ReceiveActor
    {
        public ProductsActor()
        {
            LogTo.Debug($"{nameof(ProductsActor)}.ctor()");

            Receive<AddProduct>(message => ReceiveMessage(message));
        }

        private void ReceiveMessage(AddProduct message)
        {
            LogTo.Debug($"{nameof(ProductsActor)}.{nameof(ReceiveMessage)}({nameof(AddProduct)} message)");
            LogTo.Debug($"todo: Add product '{message.Name}'.");
        }
    }
}