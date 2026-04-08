using Automation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Serilog;
using Automation.Infrastructure.Security;
using Automation.Worker.Services;

namespace Automation.Worker
{
    public class RobotRunner
    {
        public static async Task RunAsync(IBrowserPage page)
        {
            var resolver = new CredentialResolver();

            // Descobre todos os robôs disponíveis no projeto Automation.Robots
            var robotTypes = Assembly.Load("Automation.Robots")
                .GetTypes()
                .Where(t => typeof(IRobot).IsAssignableFrom(t) && !t.IsInterface);

            // Lê os clientes
            var jsonClientes = await File.ReadAllTextAsync(@"C:\AutomaFiscal\clientes.json");
            var clientesConfig = JsonSerializer.Deserialize<ClientesConfig>(jsonClientes)!;

            foreach (var cliente in clientesConfig.Clientes)
            {
                Log.Information("Iniciando execução para cliente {Cliente}", cliente.Nome);

                foreach (var robotName in cliente.Robots)
                {
                    var type = robotTypes.FirstOrDefault(t => t.Name == robotName);

                    if (type == null)
                    {
                        Log.Warning("Robô {Robot} não encontrado.", robotName);
                        continue;
                    }

                    var robot = (IRobot)Activator.CreateInstance(type)!;

                    var pastaRobot = FolderStructure.GetRobotFolder(cliente.Nome, robot.Name);

                    var context = new RobotContext
                    {
                        Cliente = cliente.Nome,
                        PastaDownload = pastaRobot,
                        Credenciais = resolver.Resolve(robot.RequiredCredentials)
                    };

                    await robot.ExecuteAsync(context, page);
                }
            }
        }
    }
}
