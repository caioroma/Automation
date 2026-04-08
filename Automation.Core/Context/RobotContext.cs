using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core
{
    public class RobotContext
    {
        public string Cliente { get; set; } = default!;
        public Dictionary<string, string> Credenciais { get; set; } = new();
        public string PastaDownload { get; set; } = default!;
    }
}
