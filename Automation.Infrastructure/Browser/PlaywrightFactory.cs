using Automation.Core;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Infrastructure.Browser
{
    public class PlaywrightFactory
    {
        private IPlaywright _playwright = null!;
        private IBrowser _browser = null!;

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new()
            {
                Headless = true
            });
        }

        public async Task<IBrowserPage> CreatePageAsync()
        {
            var context = await _browser.NewContextAsync();
            var page = await context.NewPageAsync();

            return new PlaywrightBrowserPage(page);
        }
    }
}
