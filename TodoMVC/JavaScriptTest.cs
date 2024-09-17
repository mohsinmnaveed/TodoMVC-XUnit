using TodoMVC.Browsers;
using TodoMVC.Pages;


[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 2)]
namespace TodoMVC
{
    //[Collection("Chrome")] //Collection can be distinctly defined
    public class JavaScriptTest : IClassFixture<FirefoxBrowser>, IClassFixture<PagesFixture>
    {
        private readonly FirefoxBrowser _fixture;
        private readonly PagesFixture _pagesFixture;

        public JavaScriptTest(FirefoxBrowser fixture, PagesFixture pagesFixture)
        {
            _fixture = fixture;
            _pagesFixture = pagesFixture;
        }


        [RetryTheory(MaxRetries = 3), TestPriority(0)]
        [Trait("Category", "E2E")]
        [Trait("TestCase", "VerifyTodoListCreatedSuccessfully")]
        [Trait("App", "JavaScript")]
        /*[InlineData("JavaScript", "React")]
        [InlineData("JavaScript", "React Redux")]
        [InlineData("JavaScript", "Vue.js")]
        [InlineData("JavaScript", "Preact")]
        [InlineData("JavaScript", "Backbone.js")]
        [InlineData("JavaScript", "Angular")]
        [InlineData("JavaScript", "Ember.js")]*/
        [InlineData("JavaScript", "Lit")]
        /*[InlineData("JavaScript", "KnockoutJS")]
        [InlineData("JavaScript", "Dojo")]
        [InlineData("JavaScript", "Knockback.js")]
        [InlineData("JavaScript", "CanJS")]
        [InlineData("JavaScript", "Polymer")]
        [InlineData("JavaScript", "Mithril")]
        [InlineData("JavaScript", "Marionette.js")]*/
        public void VerifyJavaScriptTodoListCreatedSuccessfully(string App, string SubApp)
        {
            _pagesFixture.HomePage.GotoTodoMVC();
            _pagesFixture.HomePage.Clickapp(SubApp);
            List<string> todoItems = new List<string>{ "Item 1", "Item 2", "Item 3"};
            _pagesFixture.TodoPage.AddTodoItems(todoItems);
            _pagesFixture.TodoPage.CheckTodoItems(todoItems.Slice(0, 1));
            Assert.True(_pagesFixture.TodoPage.VerifyItemsLeft(2));
        }
    }
}