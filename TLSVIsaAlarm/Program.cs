using System;
using System.IO;
using System.Threading.Tasks;
using Common;
using OpenQA.Selenium.Chrome;

namespace TLSVIsaAlarm;

class Program
{
    static async Task Main()
    {
        var paramLines = await File.ReadAllLinesAsync("Params.txt");
        int.TryParse(paramLines.GetLine(0), out var interval);
        var userDataDir = paramLines.GetLine(1);
        var profileDir = paramLines.GetLine(2);
        var url = paramLines.GetLine(3);
        //DateTime.TryParse(paramLines.GetLine(7), out var latestDate);
        var driver = Helpers.CreateDriver(userDataDir, profileDir);

        while (true)
        {
            Console.WriteLine("Last iteration time: " + DateTime.Now);
            
            driver.Url = url;
            await Helpers.Wait(5);
            while (driver.FindByXPath("//a[text() = 'My Application']").Count == 0)
                await Helpers.Wait(1);

            driver.ClickByXPath("//button[contains(text(), 'VIEW GROUP')]");
            await WaitForOverlay(driver);
        
            driver.ExecuteScript("window.scrollTo(0, 500)");
            await Helpers.Wait(5);
            
            driver.ClickByXPath("//button[contains(text(), 'Book appointment')]");
            await WaitForOverlay(driver);


            var slots = driver.FindByXPath("//button[contains(@class, 'tls-time-unit  -available') and not(contains(@class, '-prime'))]");
            var modal = driver.FindByXPath(
                "//div[text() = 'Sorry, there is no available appointment at the moment, please check again later.']");
            if (modal.Count == 0 && slots.Count > 0)
            {
                Console.WriteLine("Found correct date!");
                Helpers.Beep(10, 6);
                Console.ReadLine();
                return;
            }

            if (modal.Count > 0)
                driver.ClickByXPath("//button[contains(text(), 'Confirm')]");

            await Helpers.Wait(interval);
        }
    }

    private static async Task WaitForOverlay(ChromeDriver? driver)
    {
        await Helpers.Wait(5);
        while (driver?.FindByXPath("//div[contains(@class, 'vld-overlay') and contains(@style, 'display: none;')]").Count == 0)
            await Helpers.Wait(1);
    }
}