using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core
{
    public interface IBrowserPage
    {
        Task GotoAsync(string url);
        Task WaitAsync(int milliseconds);
        Task ScreenshotAsync(string path);
        Task FillAsync(string selector, string value);
        Task ClickAsync(string selector);
        Task WaitForSelectorAsync(string selector);
        Task<string?> GetAttributeAsync(string selector, string attribute);
        Task<string?> GetTextAsync(string selector);
        Task<(bool success, string? message)> ReadFlashMessageAsync(string selector);
        Task DownloadFileAsync(string clickSelector, string pastaDestino, string tipoArquivo, string cnpj);
    }
}
