using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TemplateApp.Presentation.Web.Test
{
    public class WebAppUnitTests
    {
        private Process _webAppProcess;
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            Directory.SetCurrentDirectory("../../../../TemplateApp.Presentation.Web/bin/Debug/net8.0");
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            _webAppProcess = Process.Start("dotnet", "TemplateApp.Presentation.Web.dll");
            _driver = new ChromeDriver();
        }

        [Test, Order(1)]
        public void IsSubmitButtonPresent()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Home/Index");
            var result = _driver.FindElement(By.Id("InputSubmit"));
            Assert.IsNotNull(result);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _webAppProcess.Kill();
        }
    }
}