namespace TodoMVC.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using System;

    public class TodoPage
    {
      
        private readonly AdapterFixture _fixture;
        private string _url = "https://todomvc.com/";

        public TodoPage(AdapterFixture fixture)
        {
            _fixture = fixture;
        }

        By _todoInputBy = By.CssSelector("input[placeholder='What needs to be done?']");
        IWebElement _todoInput;

        public void SetTodoInput() {
            try
            {
                _todoInput = _fixture.WaitForElementToBeClickable(_todoInputBy);
            }
            catch (TimeoutException ex)
            {
                //If the element is shadowed
                _todoInput = _fixture.GetElementUnderShadowRoot(_shadowHostTodoForm, _todoInputBy);
            }
            catch {
            
            }
        }

        By _shadowHostTodoAppBy = By.CssSelector("todo-app");
        IWebElement _shadowHostTodoApp => _fixture.WaitForElementToBeClickable(_shadowHostTodoAppBy);
        
        By _shadowHostTodoFormBy = By.CssSelector("todo-form");
        IWebElement _shadowHostTodoForm => _fixture.GetElementUnderShadowRoot(_shadowHostTodoApp, _shadowHostTodoFormBy);

        By _shadowHostTodoListBy = By.CssSelector("todo-list[class='show-priority']");
        IWebElement _shadowHostTodoList => _fixture.GetElementUnderShadowRoot(_shadowHostTodoApp, _shadowHostTodoListBy);

        By _shadowHostTodoItemBy = By.CssSelector("todo-item");
        IReadOnlyCollection<IWebElement> _shadowHostTodoItems => _fixture.GetElementsUnderShadowRoot(_shadowHostTodoList, _shadowHostTodoItemBy);

        By _shadowHostTodoFooterBy = By.CssSelector("todo-footer");
        IWebElement _shadowHostTodoFooter => _fixture.GetElementUnderShadowRoot(_shadowHostTodoApp, _shadowHostTodoFooterBy);

        By _todoDoneInputBy;
        IWebElement _todoDoneInput;

        public void SetTodoDoneInput(string item)
        {
            try
            {
                _todoDoneInputBy = By.XPath($"//label[text()='{item}']/preceding-sibling::input");
                _todoDoneInput = _fixture.FindElement(_todoDoneInputBy);
            }
            catch {
                //If element is shadowed
                _todoDoneInputBy = By.CssSelector("input[class*='toggle']");
                foreach (IWebElement todoItem in _shadowHostTodoItems)
                {
                    if (_fixture.GetElementUnderShadowRoot(todoItem, By.CssSelector("label")).Text.Equals(item))
                    {
                        _todoDoneInput = _fixture.GetElementUnderShadowRoot(todoItem, _todoDoneInputBy);
                    }
                }
            }
        }

        By _todoCountBy;
        IWebElement _todoCount;

        public void SetTodoCount() { 
            _todoCountBy = By.CssSelector("span[class*='todo-count']");
            try
            {
                _todoCount = _fixture.WaitForElementToBeVisible(_todoCountBy);
            }
            catch {
                _todoCount = _fixture.GetElementUnderShadowRoot(_shadowHostTodoFooter, _todoCountBy);
            }
        }
        public void WaitForTodoPageToLoad()
        {
            SetTodoInput();
        }

        public void AddTodoItems(List<string> items)
        {
            foreach (string item in items) {
                SetTodoInput();
                _fixture.actions.Click(_todoInput).SendKeys(item).SendKeys(Keys.Enter).Perform();
            }
        }

        public void CheckTodoItems(List<string> itemsToSelect) {
            foreach (string itemtoSelect in itemsToSelect) {
                SetTodoDoneInput(itemtoSelect);
                _todoDoneInput.Click();
            }
        }

        public bool VerifyItemsLeft(int expectedLeftItems)
        {
            SetTodoCount();
            int.TryParse(_todoCount.Text.Split(" ")[0], out int result);
            return result == expectedLeftItems;
        }
    }
}
