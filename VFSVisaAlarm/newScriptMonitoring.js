function createBeeper() {
    let context = new AudioContext();
    let ocsillator = context.createOscillator();
    ocsillator.type = 'sine';
    ocsillator.frequency.value = 440;
    ocsillator.connect(context.destination);
    return ocsillator;
}

function beep(ocsillator, durationS) {
    ocsillator.start();
    ocsillator.stop(context.currentTime + durationS);
};
function delay(minS, maxS) {
    const ms = Math.random() * (maxS - minS) + minS;
    return new Promise(resolve => setTimeout(resolve, ms * 1000));
}
function delayS() { return delay(1, 3); }
function delayM() { return delay(7, 10); }
function delayL() { return delay(15, 20); }

function triggerInputEvent(inputElement) {
    inputElement.dispatchEvent(new Event('input'));
}

function reload() {
    window.location.href = window.location.href;
}

async function clickLargeBtn() {
    await delayS();
    $(".mat-btn-lg").focus();
    await delayS();
    $(".mat-btn-lg").click();
}

async function enterCreds() {
    await delayM();
    $("#password").focus();
    await delayS();

    const pass = "s^Uper1pass23?".split('');
    for (const char of pass) {
        if (char === "^")
            $("button.mat-button-base:contains('keyboard_arrow_up')").click();
        else {
            $("button.mat-button-base:not(.mat-keyboard-key-modifier):contains('" + char +"')").click();
            await delayS();
        }
    }

    await clickLargeBtn();
}

async function enterOtp() {
    await delayM();
    const otp = await navigator.clipboard.readText();
    await delayS();
    $("#mat-input-5").val(otp);
    triggerInputEvent($("#mat-input-5")[0]);
    await clickLargeBtn();
}

async function findSlots() {
    await delayM();
    let beeper = createBeeper();
    $("div#mat-select-value-1").click();
    await delayS();
    $("#HUN-LON").click();
    await delayM();
    $("div#mat-select-value-5").click();
    await delayS();
    $("#oth").click();
    await delayM();
    $("div#mat-select-value-3").click();
    await delayS();
    $("#TA").click();
    await delayM();
    if ($("div.alert:contains('no appointment slots are currently available')").length === 0) {
        beep(beeper, 600);
    }
}

async function goToBooking() {
    await delayM();
    await clickLargeBtn();
}

async function monitoringLoop() {
    await enterCreds();
    await enterOtp();
	await goToBooking();
    await findSlots();
    await delay(60*30, 60*31);
    await reload();
}

//monitoringLoop();