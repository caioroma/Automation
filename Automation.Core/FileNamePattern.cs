using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core
{
    public static class FileNamePattern
    {
        public static string Build(
            string tipo,
            string cnpj,
            string extensao)
        {
            var data = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            return $"{tipo}_{cnpj}_{data}.{extensao}";
        }
    }
}
