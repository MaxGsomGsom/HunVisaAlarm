import { Builder, By, WebDriver, WebElement, until } from 'selenium-webdriver';
import chrome from 'selenium-webdriver/chrome.js';
import path from 'path';
import clipboardy from 'clipboardy';

async function findByAriaLabel(driver: WebDriver, ariaLabelSubstring: string): Promise<WebElement | undefined> {
    try {
        const elements = await driver.findElements(By.css('[aria-label]'));
        for (const el of elements) {
            const ariaLabel = await el.getAttribute('aria-label');
            if (ariaLabel.includes(ariaLabelSubstring))
                return el;
        }
    } catch {
        // ignore
    }
    return;
}

function delay(minS: number, maxS: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, (Math.random() * (maxS - minS) + minS) * 1000));
}

async function runSelenium() {
    const userDataDir = path.resolve('dataDir', 'chrome-profile');
    const options = new chrome.Options();
    options.addArguments(`--user-data-dir=${userDataDir}`, '--profile-directory=Default');

    const driver = await new Builder().forBrowser('chrome').setChromeOptions(options).build();

    try {
        await driver.get('https://outlook.live.com/mail/0/');
        await driver.wait(until.titleIs('Почта — Kuzmin Max — Outlook'), 100000);
        await delay(10, 10);
        let oldTime;

        while (true) {
            await delay(1, 1);
            const element = await findByAriaLabel(driver, "VFS Global One Time Password");
            if (!element) continue;

            const fullText = await element.getAttribute('aria-label');
            const newTime = /\d?\d:\d\d/.exec(fullText)?.[0];
            if (!newTime || newTime === oldTime) continue;

            const otpPass = /\d{6}/.exec(fullText)?.[0];
            if (otpPass) {
                clipboardy.writeSync(otpPass);
                oldTime = newTime;
            }
        }
    } finally {
        await driver.quit();
    }
}

runSelenium().catch(console.error);
