using TodoMVC.Browsers;
using TodoMVC.Pages;


namespace TodoMVC
{
    //[Collection("Chrome")] //Collection can be distinctly defined
    public class CompileToJSTest : IClassFixture<ChromeBrowser>, IClassFixture<PagesFixture>
    {
        private readonly ChromeBrowser _fixture;
        private readonly PagesFixture _pagesFixture;

        public CompileToJSTest(ChromeBrowser fixture, PagesFixture pagesFixture)
        {
            _fixture = fixture;
            _pagesFixture = pagesFixture;
        }

        //[RetryTheory(MaxRetries = 3), TestPriority(0)]
        [Theory]
        [Trait("Category", "E2E")]
        [Trait("TestCase", "VerifyCompileToJSTodoListCreatedSuccessfully")]
        [Trait("App", "Compile-to-JS")]
        [InlineData("Compile-to-JS", "Svelte")]
        [InlineData("Compile-to-JS", "GWT")]
        [InlineData("Compile-to-JS", "Closure")]
        [InlineData("Compile-to-JS", "Elm")]
        [InlineData("Compile-to-JS", "AngularDart")]
        [InlineData("Compile-to-JS", "TypeScript + Backbone.js")]
        [InlineData("Compile-to-JS", "TypeScript + AngularJS")]
        [InlineData("Compile-to-JS", "TypeScript + React")]
        [InlineData("Compile-to-JS", "Reagent")]
        [InlineData("Compile-to-JS", "Scala.js + Binding.scala")]
        [InlineData("Compile-to-JS", "js_of_ocaml")]
        [InlineData("Compile-to-JS", "Humble + GopherJS")]
        public void VerifyCompileToJSTodoListCreatedSuccessfully(string App, string SubApp)
        {
            _pagesFixture.HomePage.GotoTodoMVC();
            _pagesFixture.HomePage.ClickTab(App);
            _pagesFixture.HomePage.Clickapp(SubApp);
            List<string> todoItems = new List<string> { "Item 1", "Item 2", "Item 3" };
            _pagesFixture.TodoPage.AddTodoItems(todoItems);
            _pagesFixture.TodoPage.CheckTodoItems(todoItems.Slice(0, 1));
            Assert.True(_pagesFixture.TodoPage.VerifyItemsLeft(2));
        }

    }
}