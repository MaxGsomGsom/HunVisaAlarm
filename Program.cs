using System;
using System.IO;
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
            var intervalInSec = int.Parse(paramLines[0]);
            var login = paramLines[1];
            var pass = paramLines[2];
            var driver = new ChromeDriver();

            while (true)
            {
                Console.WriteLine("Last iteration time: " + DateTime.Now);
                driver.Url = "https://visa.vfsglobal.com/rus/ru/hun/login";
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath("//button[@id='onetrust-accept-btn-handler']")).Click();
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath("//input[@formcontrolname='username']")).SendKeys(login);
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath("//input[@formcontrolname='password']")).SendKeys(pass);
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath("//button[@mat-raised-button]")).Click();
                Thread.Sleep(Sec(10));
                driver.FindElements(By.XPath("//button[@mat-raised-button]"))[1].Click();
                Thread.Sleep(Sec(10));
                driver.FindElement(By.XPath("//div[@id='mat-select-value-1']")).Click();
                Thread.Sleep(Sec(5));
                driver.FindElements(By.XPath("//span[@class='mat-option-text']"))[0].Click();
                Thread.Sleep(Sec(5));
                driver.ExecuteScript("window.scrollTo(0, 99999)");
                Thread.Sleep(Sec(5));
                driver.FindElement(By.XPath("//div[@id='mat-select-value-5']")).Click();
                Thread.Sleep(Sec(10));
                driver.FindElements(By.XPath("//span[@class='mat-option-text']"))[4].Click();
                Thread.Sleep(Sec(10));

                if (driver.FindElements(By.XPath("//div[@class='alert alert-info border-0 rounded-0']")).Count == 0)
                {
                    Console.Beep(1000, Sec(10));
                    Console.ReadLine();
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
