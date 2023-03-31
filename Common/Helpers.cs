using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace Common;

public static class Helpers
{
    public static ChromeDriver CreateDriver(string? userDataDir, string? profileDir)
    {
        var options = new ChromeOptions();
        if (userDataDir != null)
            options.AddArgument($@"user-data-dir={userDataDir}");
        if (profileDir != null)
            options.AddArgument($"--profile-directory={profileDir}");
        options.AddExcludedArgument("enable-automation");
        return new ChromeDriver(options);
    }
        
    public static string? GetLine(this string[] paramLines, int num)
    {
        if (paramLines.Length < num - 1)
            return null;
        return paramLines[num].Split('#')[0].Trim();
    }
        
    public static Task Wait(int sec)
    {
        return Task.Delay(Ms(sec));
    }

    private static int Ms(int sec)
    {
        return new Random().Next(sec * 1000 - 500, sec * 1000 + 500);
    }

    public static void ClickByXPath(this ChromeDriver driver, string xpath)
    {
        var actions = new Actions(driver);
        var element = driver.FindElement(By.XPath(xpath));
        actions.MoveToElement(element).MoveByOffset(10, 10).Click().Perform();
    }
        
    public static ReadOnlyCollection<IWebElement> FindByXPath(this ChromeDriver driver, string xpath)
    {
        return driver.FindElements(By.XPath(xpath));
    }

    public static void Beep(int sec, int times)
    {
        for (int i = 0; i < times; i++)
        {
            Console.Beep(1000, Ms(sec));
        }
    }
}