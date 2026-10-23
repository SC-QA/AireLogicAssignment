using Microsoft.Playwright;

namespace CookieCutterFramework.Pages.Base
{
    public partial class BasePage : PageTest
    {
        public IPage _page;

        public BasePage(IPage page)
        {
            _page = page;
        }
    }
}
