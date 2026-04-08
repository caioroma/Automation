using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core
{
    public static class FolderStructure
    {
        public static string GetRobotFolder(string cliente, string robotName)
        {
            var basePath = @"C:\AutomaFiscal\Clientes";

            var path = Path.Combine(basePath, cliente, robotName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}