using Automation.Infrastructure.Browser;
using Automation.Worker;
using Serilog;

var basePath = @"C:\AutomaFiscal";

Directory.CreateDirectory(basePath);
Directory.CreateDirectory(Path.Combine(basePath, "logs"));
Directory.CreateDirectory(Path.Combine(basePath, "downloads"));

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        Path.Combine(basePath, "logs", "log-.txt"),
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Iniciando execução dos robôs");

    var factory = new PlaywrightFactory();
    await factory.InitializeAsync();

    var page = await factory.CreatePageAsync();

    await RobotRunner.RunAsync(page);

    Log.Information("Execução finalizada com sucesso");
}
catch (Exception ex)
{
    Log.Error(ex, "Erro na execução");
}
finally
{
    Log.CloseAndFlush();
}