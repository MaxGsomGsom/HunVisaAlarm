using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common;
using OpenQA.Selenium.Chrome;

var paramLines = await File.ReadAllLinesAsync("Params.txt");
int.TryParse(paramLines.GetLine(0), out var interval);
var userDataDir = paramLines.GetLine(1);
var profileDir = paramLines.GetLine(2);
var url = paramLines.GetLine(3);
var visaCenter = paramLines.GetLine(4);
var visaType = paramLines.GetLine(5);
var visaSubType = paramLines.GetLine(6);
DateTime.TryParse(paramLines.GetLine(7), out var latestDate);
int errorsCount = 0;

Console.WriteLine("=== Initialization completed");
await Helpers.Beep(0.5, 1);

while (true)
{
    ChromeDriver? driver = null;
    try
    {
        Console.WriteLine("=== Last try: " + DateTime.Now);
        driver = Helpers.CreateDriver(userDataDir, profileDir);
        if (!await Iterate(driver))
            return;
    }
    catch (Exception e)
    {
        await Helpers.Beep(0.5, 1);
        Console.WriteLine("=== Exception: " + e);
        if (errorsCount++ >= 5)
            return;
    }
    finally
    {
        driver?.Close();
        await Helpers.Wait(interval);
    }
}

async Task<bool> Iterate(ChromeDriver driver)
{
    driver.Url = url;
    await Helpers.Wait(5);

    // cookies
    await Helpers.Wait(5);
    if (driver.FindByXPath("//button[@id='onetrust-accept-btn-handler']").Any())
        driver.ClickByXPath("//button[@id='onetrust-accept-btn-handler']");

    // recaptcha
    await Helpers.Wait(5);
    driver.SwitchTo().Frame(driver.FindByXPath("//iframe[@title='reCAPTCHA']").First());
    driver.ClickByXPath("//span[contains(@class, 'recaptcha-checkbox')]");
    await Helpers.Wait(5);
    if (!driver.FindByXPath("//span[contains(@class, 'recaptcha-checkbox-checked')]").Any())
    {
        driver.SwitchTo().ParentFrame();
        driver.SwitchTo().Frame(driver.FindByXPath("//iframe[@title='recaptcha challenge expires in two minutes']")
            .First());
        await Helpers.Wait(5);
        driver.ClickByXPath("//div[@class='button-holder help-button-holder']");
        await Helpers.Wait(15);
    }

    // login button
    driver.SwitchTo().ParentFrame();
    driver.ClickByXPath("//button[@mat-stroked-button]");
    await Helpers.Wait(15);

    // start application button
    driver.FindByXPath("//button[@mat-raised-button]").Last().Click();
    await Helpers.Wait(15);

    // visa center input
    driver.ClickByXPath("//div[@id='mat-select-value-1']");
    await Helpers.Wait(5);
    driver.ClickByXPath($"//mat-option/*[contains(text(), '{visaCenter}')]");
    await Helpers.Wait(5);

    // visa type input
    driver.ClickByXPath("//div[@id='mat-select-value-3']");
    await Helpers.Wait(5);
    driver.ClickByXPath($"//mat-option/*[contains(text(), '{visaType}')]");
    await Helpers.Wait(5);

    // visa sub type input
    driver.ExecuteScript("window.scrollTo(0, 99999)");
    await Helpers.Wait(5);
    driver.ClickByXPath("//div[@id='mat-select-value-5']");
    await Helpers.Wait(5);
    driver.ClickByXPath($"//mat-option/*[contains(text(), '{visaSubType}')]");
    await Helpers.Wait(10);

    // parse date
    var dateElement = driver.FindByXPath("//div[@class='alert alert-info border-0 rounded-0']")
        .FirstOrDefault();
    if (dateElement != null)
    {
        var dateString = Regex.Matches(dateElement.Text, "\\d{2}-\\d{2}-\\d{4}").FirstOrDefault()?.Value;

        if (dateString != null)
            Console.WriteLine("=== Found date: " + dateString);

        if (dateString != null && DateTime.Parse(dateString) < latestDate)
        {
            Console.WriteLine("=== Found correct date!");
            await Helpers.Beep(10, 6);
            Console.ReadLine();
            return false;
        }
    }
    return true;
}