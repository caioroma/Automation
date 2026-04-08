using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Worker
{
    public class RobotConfig
    {
        public List<RobotItem> Robots { get; set; } = new();
    }

    public class RobotItem
    {
        public string Name { get; set; } = default!;
        public bool Ativo { get; set; }
    }
}
