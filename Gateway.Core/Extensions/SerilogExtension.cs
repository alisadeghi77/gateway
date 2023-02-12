using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Core.Extensions
{
    public static class SerilogExtension
    {
        private static LoggerConfiguration ExcludeSource(this LoggerConfiguration loggerConfiguration,
            IEnumerable<string> sources)
        {
            foreach (var source in sources)
            {
                loggerConfiguration.Filter.ByExcluding(Matching.FromSource(source));
            }

            return loggerConfiguration;
        }

        public static void CreateSeriLog(IConfiguration configuration, IEnumerable<string> sources)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ExcludeSource(sources)
                .WriteTo.ColoredConsole(
                    LogEventLevel.Verbose,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
                .Enrich.WithExceptionDetails().Enrich.WithMachineName()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
                {
                    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.RaiseCallback,
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    IndexFormat = configuration["ElasticConfiguration:IndexFormat"],
                    CustomFormatter = new ExceptionAsObjectJsonFormatter()
                })
                .CreateLogger();
        }
    }
}