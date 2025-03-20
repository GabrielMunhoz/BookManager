using BookManager.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Display;
using Serilog.Sinks.MSSqlServer;

namespace BookManager.IoC.Extensions;
public static class SerilogExtension
{
    private const string OutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
    public static void LogsConfig(Microsoft.AspNetCore.Builder.WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .DefineSqlLogger(builder.Configuration.GetConnectionString(ConstantsConf.BookConnection))
            .CreateLogger();

        builder.Host.UseSerilog();
        builder.Services.AddLogging(logBuilder => logBuilder.AddSerilog(dispose: true));
    }

    public static LoggerConfiguration DefineSqlLogger(this LoggerConfiguration config, string connection)
    {
        var sinkOptions = new MSSqlServerSinkOptions
        {
            SchemaName = "BookManager",
            TableName = "Logs",
            AutoCreateSqlTable = true,
        };

        var columnsOption = new ColumnOptions();
        columnsOption.Store.Add(StandardColumn.LogEvent);
        columnsOption.Store.Remove(StandardColumn.Properties);
        columnsOption.LogEvent.DataLength = 2048;
        columnsOption.Id.DataType = System.Data.SqlDbType.BigInt;

        return config
            .WriteTo
            .MSSqlServer(connectionString: connection,
                sinkOptions: sinkOptions,
                columnOptions: columnsOption,
                formatProvider: null,
                logEventFormatter: new MessageTemplateTextFormatter(OutputTemplate));
    }
}
