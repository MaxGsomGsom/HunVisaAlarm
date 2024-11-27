function findByAriaLabel(ariaLabelSubstring) {
    const elements = document.querySelectorAll('[aria-label]'); 
    return Array.from(elements).find(el => 
        el.getAttribute('aria-label').includes(ariaLabelSubstring)
    );
}

function delay(minS, maxS) {
    const ms = Math.random() * (maxS - minS) + minS;
    return new Promise(resolve => setTimeout(resolve, ms * 1000));
}

function copyTextToClipboard(text) {
    const textArea = document.createElement("textarea");
    textArea.value = text;
    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand('copy');
    document.body.removeChild(textArea);
}

async function monitoringLoop() {
    while (true) {
        await delay(1, 1);
        const fullText = findByAriaLabel("VFS Global One Time Password")?.ariaLabel;
        if (!fullText)
            continue;
        const newTime = /\d?\d:\d\d/.exec(fullText)[0];
        if (newTime == window.oldTime)
            continue;

        const otpPass = /\d{6}/.exec(fullText)[0];
        copyTextToClipboard(otpPass);
    }
}