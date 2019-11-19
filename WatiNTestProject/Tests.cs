using NUnit.Framework;
using System;
using System.Threading;
using WatiN.Core;
using WatiN.Core.Native.Windows;

namespace Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class Tests
    {
        private IE _driver;
        private const string Url = "https://www.google.com/";

        private readonly Uri _uri = new Uri(Url);

        private const string SEARCH_FIELD_CSS = "input[class = 'gLFyf gsfi']";

        private const string SEARCH_BUTTON_CSS = "div[class = 'FPdoLc tfB0Bf'] input[class = 'gNO89b']";

        private const string WIKI_RESULT_CSS = "div[class = 'srg'] a[href $= 'wiki/XPath']";

        private const string CONTEXT_LIST_CSS = "div[id= 'toc']>ul a";

        [OneTimeSetUp]
        public void Setup()
        {
            _driver = new IE();
            _driver.ShowWindow(NativeMethods.WindowShowStyle.ShowMaximized);
        }

        [Test]
        public void Test1()
        {
            _driver.NativeBrowser.NavigateTo(_uri);
            WaitUntilDriverIsFree();

            _driver.TextField(Find.BySelector(SEARCH_FIELD_CSS)).TypeText("xpath");
            WaitUntilDriverIsFree();

            _driver.Element(Find.BySelector(SEARCH_BUTTON_CSS)).Click();
            WaitUntilDriverIsFree();

            _driver.Element(Find.BySelector(WIKI_RESULT_CSS)).Click();
            WaitUntilDriverIsFree();

            var allContextElements = _driver.Links.Filter(Find.BySelector(CONTEXT_LIST_CSS));

            var contextListLength = allContextElements.Count;
            Assert.IsTrue(contextListLength == 36, $"Number of elements in context should be 36, but it is {contextListLength}");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Close();
        }

        private void WaitUntilDriverIsFree()
        {
            if (( (SHDocVw.InternetExplorerClass) _driver.InternetExplorer).Busy)
                _driver.WaitForComplete();
        }
    }
}