using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core
{
    public class ClientesConfig
    {
        public List<ClienteConfig> Clientes { get; set; } = new();
    }

    public class ClienteConfig
    {
        public string Nome { get; set; } = string.Empty;
        public List<string> Robots { get; set; } = new();
    }
}
