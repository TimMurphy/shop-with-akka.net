using System;
using Serilog.Core;
using Serilog.Events;

namespace ConsoleApplication.Logging.Enrichers
{
    internal class ElapsedTimeEnricher : ILogEventEnricher
    {
        public const string ElapsedTimePropertyName = "ElapsedTime";
        private static readonly DateTime Started = DateTime.Now;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(new LogEventProperty(
                ElapsedTimePropertyName,
                new ScalarValue(DateTime.Now.Subtract(Started))));
        }
    }
}