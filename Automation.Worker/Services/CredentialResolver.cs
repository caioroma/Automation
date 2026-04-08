using Automation.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Worker.Services
{
    public class CredentialResolver
    {
        private readonly CredentialService _service = new();

        public Dictionary<string, string> Resolve(IEnumerable<string> required)
        {
            var result = new Dictionary<string, string>();

            foreach (var key in required)
            {
                var value = _service.Get(key);

                if (string.IsNullOrEmpty(value))
                {
                    value = AskWithConfirmation(key);
                    _service.Save(key, value);
                }

                result[key] = value!;
            }

            return result;
        }

        private string AskWithConfirmation(string key)
        {
            while (true)
            {
                Console.Write($"Informe {key}: ");
                var v1 = Console.ReadLine();

                Console.Write($"Confirme {key}: ");
                var v2 = Console.ReadLine();

                if (v1 == v2 && !string.IsNullOrWhiteSpace(v1))
                    return v1;

                Console.WriteLine("Valores não conferem.\n");
            }
        }
    }
}
