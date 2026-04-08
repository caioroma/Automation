using Automation.Core;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Infrastructure.Browser
{
    public class PlaywrightBrowserPage : IBrowserPage
    {
        private readonly IPage _page;

        public PlaywrightBrowserPage(IPage page)
        {
            _page = page;
        }

        public Task GotoAsync(string url)
            => _page.GotoAsync(url);

        public Task WaitAsync(int milliseconds)
            => _page.WaitForTimeoutAsync(milliseconds);

        public Task ScreenshotAsync(string path)
            => _page.ScreenshotAsync(new() { Path = path });

        public Task FillAsync(string selector, string value)
            => _page.FillAsync(selector, value);

        public Task ClickAsync(string selector)
            => _page.ClickAsync(selector);

        public Task WaitForSelectorAsync(string selector)
            => _page.WaitForSelectorAsync(selector);

        public async Task<string?> GetAttributeAsync(string selector, string attribute)
        {
            var el = await _page.QuerySelectorAsync(selector);
            return el == null ? null : await el.GetAttributeAsync(attribute);
        }

        public async Task<string?> GetTextAsync(string selector)
        {
            var el = await _page.QuerySelectorAsync(selector);

            if (el == null)
                return null;

            return await el.InnerTextAsync();
        }

        public async Task<(bool success, string? message)> ReadFlashMessageAsync(string selector)
        {
            await _page.WaitForSelectorAsync(selector);

            var element = await _page.QuerySelectorAsync(selector);

            if (element == null)
                return (false, null);

            var classes = await element.GetAttributeAsync("class");
            var text = await element.InnerTextAsync();

            var success =
                classes != null &&
                (classes.Contains("success") || classes.Contains("sucesso"));

            return (success, text?.Trim());
        }

        public async Task DownloadFileAsync(string clickSelector, string pastaDestino, string tipoArquivo, string cnpj)
        {
            var downloadTask = _page.WaitForDownloadAsync();

            await _page.ClickAsync(clickSelector);

            var download = await downloadTask;

            var extensao = Path.GetExtension(download.SuggestedFilename);

            var nomeArquivo = FileNamePattern.Build(tipoArquivo, cnpj, extensao.Trim('.'));

            var caminhoFinal = Path.Combine(pastaDestino, nomeArquivo);

            await download.SaveAsAsync(caminhoFinal);
        }
    }
}
