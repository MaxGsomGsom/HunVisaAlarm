using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;

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
        DateTime.TryParse(paramLines.GetLine(7), out var latestDate);
        var driver = Helpers.CreateDriver(userDataDir, profileDir);

        driver.Url = url;
        await Helpers.Wait(5);
        while (driver.FindByXPath("//a[text() = 'My account']").Count == 0)
            await Helpers.Wait(1);
        await Helpers.Wait(5);
        
        Console.WriteLine("Initialization completed");

        while (true)
        {
            Console.WriteLine("Last iteration time: " + DateTime.Now);
            
            driver.ClickByXPath("//a[text() = 'My account']");
            await Helpers.Wait(10);
            
            var dateElement = driver.FindByXPath(
                    "//a[contains(@class, 'appt-table-btn') and not(contains(@class, 'full'))]" +
                    "/preceding-sibling::span[@class='year-month-title']")
                .FirstOrDefault();
            
            if (dateElement != null)
            {
                var dateString = dateElement.Text.Split("<br>").FirstOrDefault();
                Console.WriteLine("Found date: " + dateString);
                if (DateTime.TryParse(dateString + DateTime.Now.Year, out var foundDate) && foundDate < latestDate)
                {
                    Console.WriteLine("Found correct date!");
                    Helpers.Beep(10, 6);
                    Console.ReadLine();
                    return;
                }
            }

            await Helpers.Wait(interval);
        }
    }
}