using Automation.Core;
using Microsoft.Playwright;
using Serilog;

namespace Automation.Robots
{
    public class NFRobot : IRobot
    {
        public string Name => "NFRobot";

        public IEnumerable<string> RequiredCredentials =>
            new[] { "usuario_teste", "senha_teste" };

        public async Task ExecuteAsync(RobotContext context, IBrowserPage page)
        {
            Log.Information("Iniciando NFRobot para o cliente {Cliente}", context.Cliente);

            try
            {
                var usuario = context.Credenciais["usuario_teste"];
                var senha = context.Credenciais["senha_teste"];

                await page.GotoAsync("https://the-internet.herokuapp.com/login");

                await page.FillAsync("#username", usuario);
                await page.FillAsync("#password", senha);
                await page.ClickAsync("button[type='submit']");

                var (ok, msg) = await page.ReadFlashMessageAsync(".flash");

                if (!ok)
                {
                    Log.Error("Falha no login: {Mensagem}", msg);
                    return;
                }

                Log.Information("Login realizado com sucesso: {Mensagem}", msg);

                var screenshotPath = Path.Combine(
                    context.PastaDownload,
                    $"login_ok_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                await page.ScreenshotAsync(screenshotPath);

                Log.Information("Screenshot salva em {Path}", screenshotPath);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro inesperado no NFRobot");
            }
        }
    }
}
