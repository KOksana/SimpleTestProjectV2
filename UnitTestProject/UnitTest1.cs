﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private static IWebDriver _driver;
        private const string URL = "https://www.google.com/";

        private const string SEARCH_FIELD_XPATH = "//input[@class = 'gLFyf gsfi']";
        private const string SEARCH_FIELD_CSS = "input[class = 'gLFyf gsfi']";

        private const string SEARCH_BUTTON_XPATH = "//div[@class = 'VlcLAe']//input[@class = 'gNO89b']";
        private const string SEARCH_BUTTON_CSS = "div[class = 'VlcLAe'] input[class = 'gNO89b']";

        private const string WIKI_RESULT_XPATH = "//h3/span[text()='XPath - Wikipedia']/../..";
        private const string WIKI_RESULT_CSS = "div[class = 'r'] a[href $= 'wiki/XPath']";

        private const string CONTEXT_LIST_XPATH = "//div[@id= 'toc']/ul//a";
        private const string CONTEXT_LIST_CSS = "div[id= 'toc']>ul a";

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var options = new ChromeOptions();
            options.AddArguments("--lang=en-GB");

            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void TestMethod1()
        {
            _driver.Navigate().GoToUrl(URL);

            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            wait.Until(drv => drv.FindElement(By.XPath(SEARCH_FIELD_XPATH)));

            _driver.FindElement(By.CssSelector(SEARCH_FIELD_CSS)).SendKeys("xpath");

            wait.Until(d => _driver.FindElement(By.CssSelector(SEARCH_BUTTON_CSS)).Displayed);

            var searchButton = _driver.FindElement(By.CssSelector(SEARCH_BUTTON_CSS));
            Actions actions = new Actions(_driver);

            actions.MoveToElement(searchButton).Build().Perform();

            _driver.FindElement(By.XPath(SEARCH_BUTTON_XPATH)).Click();

            wait.Until(drv => drv.FindElement(By.XPath(WIKI_RESULT_XPATH)));

            _driver.FindElements(By.CssSelector(WIKI_RESULT_CSS))[0].Click();

            wait.Until(drv => drv.FindElement(By.ClassName("toctitle")));

            var allContextElements = _driver.FindElements(By.XPath(CONTEXT_LIST_XPATH));

            var contextListLength = allContextElements.Count;
            Assert.IsTrue(contextListLength == 36, $"Number of elements in context should be 36, but it is {contextListLength}");
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _driver.Quit();
        }
    }
}
