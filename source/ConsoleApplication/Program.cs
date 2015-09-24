using System;
using Akka.Actor;
using Anotar.Serilog;
using ConsoleApplication.Actors;
using ConsoleApplication.Logging.Enrichers;
using ConsoleApplication.Messages;
using Serilog;
using Serilog.Enrichers;

namespace ConsoleApplication
{
    internal class Program
    {
        private const string ActorSystemName = "ActorSystem";
        private static ActorSystem _actorSystem;

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once NotAccessedField.Local
        private static ILogger logger;

        private static void Main()
        {
            ConfigureLogging();

            LogTo.Debug($"Creating ActorSystem '{ActorSystemName}'.");
            _actorSystem = ActorSystem.Create(ActorSystemName);

            LogTo.Debug($"Creating Props '{nameof(ProductsActor)}'.");
            var props = Props.Create<ProductsActor>();

            LogTo.Debug($"Creating ActorOf '{nameof(ProductsActor)}'.");
            var products = _actorSystem.ActorOf(props);

            LogTo.Information("Adding products.");
            products.Tell(new AddProduct("Product 1"));
            products.Tell(new AddProduct("Product 2"));

            LogTo.Debug("Stopping products actor.");
            products.GracefulStop(TimeSpan.FromMinutes(1)).Wait();
            LogTo.Debug("Stopped products actor.");

            LogTo.Debug("Shutting down ActorSystem");
            _actorSystem.Shutdown();

            LogTo.Debug("Waiting for ActorSystem to complete shutdown.");
            _actorSystem.AwaitTermination();

            LogTo.Information("Finished shopping :-)");
        }

        private static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                       .MinimumLevel.Verbose()
                       .WriteTo.ColoredConsole(outputTemplate: "[Thread {ThreadId:00000}] [{Level}] [{ElapsedTime}] {Message}{NewLine}{Exception}")
                       .Enrich.With(new ThreadIdEnricher(), new ElapsedTimeEnricher())
                       .CreateLogger();

            logger = Log.ForContext<Program>();
        }

        private static void WaitForUserToPressEnter()
        {
            Console.WriteLine();
            Console.WriteLine("Finished shopping :-)");
            Console.WriteLine();
            Console.WriteLine("Press enter to exit...");
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}