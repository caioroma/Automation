using Automation.Infrastructure.Security;
using Automation.Infrastructure.Browser;
using Automation.Worker;
using Serilog;

var basePath = @"C:\AutomaFiscal";

Directory.CreateDirectory(basePath);
Directory.CreateDirectory(Path.Combine(basePath, "logs"));
Directory.CreateDirectory(Path.Combine(basePath, "downloads"));

if (args.Length > 0 && args[0].ToLower() == "setup")
{
    Console.WriteLine("=== Setup de Credenciais AutomaFiscal ===");

    var cred = new CredentialService();

    Console.Write("Usuário: ");
    var usuario = Console.ReadLine();

    string senha;
    string confirmacao;

    do
    {
        Console.Write("Senha: ");
        senha = LerSenhaOculta();

        Console.Write("Confirmar Senha: ");
        confirmacao = LerSenhaOculta();

        if (senha != confirmacao)
            Console.WriteLine("Senhas não conferem. Tente novamente.");
    }
    while (senha != confirmacao);

    cred.Save("usuario_teste", usuario!);
    cred.Save("senha_teste", senha);

    Console.WriteLine("Credenciais salvas com sucesso!");
    return;
}

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

static string LerSenhaOculta()
{
    var senha = string.Empty;
    ConsoleKeyInfo key;

    do
    {
        key = Console.ReadKey(true);

        if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
        {
            senha += key.KeyChar;
            Console.Write("*");
        }
        else if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
        {
            senha = senha[0..^1];
            Console.Write("\b \b");
        }
    }
    while (key.Key != ConsoleKey.Enter);

    Console.WriteLine();
    return senha;
}