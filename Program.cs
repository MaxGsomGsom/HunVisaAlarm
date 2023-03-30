using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace VisaAlarm
{
    class Program
    {
        static void Main(string[] args)
        {
            var paramLines = File.ReadAllLines("Params.txt");
            string GetLine(int num) => paramLines[num].Split("#")[0].Trim();
            var intervalInSec = int.Parse(GetLine(0));
            var login = GetLine(1);
            var pass = paramLines[2];
            var url = GetLine(3);
            var visaCenter = GetLine(4);
            var visaType = GetLine(5);
            var visaSubType = GetLine(6);
            var latestDate = DateTime.Parse(GetLine(7));
            var driver = new ChromeDriver();
            
            driver.Url = url;
            Thread.Sleep(Sec(5));
            driver.FindElement(By.XPath("//button[@id='onetrust-accept-btn-handler']")).Click();
            Thread.Sleep(Sec(5));
            driver.FindElement(By.XPath("//input[@formcontrolname='username']")).SendKeys(login);
            Thread.Sleep(Sec(5));
            driver.FindElement(By.XPath("//input[@formcontrolname='password']")).SendKeys(pass);
            Thread.Sleep(Sec(5));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='reCAPTCHA']")));
            driver.FindElement(By.XPath("//span[contains(@class, 'recaptcha-checkbox')]")).Click();
            Thread.Sleep(Sec(5));
            while (driver.FindElements(By.XPath("//span[contains(@class, 'recaptcha-checkbox-checked')]")).Count == 0)
                Thread.Sleep(Sec(1));
            driver.SwitchTo().ParentFrame();
            driver.FindElement(By.XPath("//button[@mat-stroked-button]")).Click();
            Thread.Sleep(Sec(10));
                
            driver.FindElements(By.XPath("//button[@mat-raised-button]"))[1].Click();
            Thread.Sleep(Sec(10));
            
            Console.WriteLine("Initialization completed");

            while (true)
            {
                Console.WriteLine("Last iteration time: " + DateTime.Now);
                driver.FindElement(By.XPath("//div[@id='mat-select-value-1']")).Click();
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath($"//mat-option/*[contains(text(), '{visaCenter}')]")).Click();
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath("//div[@id='mat-select-value-3']")).Click();
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath($"//mat-option/*[contains(text(), '{visaType}')]")).Click();
                Thread.Sleep(Sec(5));
                driver.ExecuteScript("window.scrollTo(0, 99999)");
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath("//div[@id='mat-select-value-5']")).Click();
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath($"//mat-option/*[contains(text(), '{visaSubType}')]")).Click();
                Thread.Sleep(Sec(10));

                var dateElement = driver.FindElements(By.XPath("//div[@class='alert alert-info border-0 rounded-0']"))
                    .FirstOrDefault();
                if (dateElement != null)
                {
                    var dateString = Regex.Matches(dateElement.Text, "\\d{2}-\\d{2}-\\d{4}").FirstOrDefault()?.Value;
                    Console.WriteLine("Found date: " + dateString);
                    if (dateString != null && DateTime.Parse(dateString) < latestDate)
                    {
                        Console.WriteLine("Found correct date!");
                        Console.Beep(1000, Sec(10));
                        Console.ReadLine();
                    }
                }

                Thread.Sleep(Sec(intervalInSec));
            }
        }

        private static int Sec(int time)
        {
            return new Random().Next(time * 1000 - 500, time * 1000 + 500);
        }
    }
}
