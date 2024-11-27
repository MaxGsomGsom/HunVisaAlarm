
async function slots(email, token) {
    let slots = await fetch("https://lift-api.vfsglobal.com/appointment/slots?countryCode=gbr&missionCode=hun&centerCode=HUN-LON&loginUser=" + email + "&visaCategoryCode=TA&languageCode=en-US&applicantsCount=1&days=180&fromDate=20%2F05%2F2023&slotType=2&toDate=16%2F11%2F2023", {
        "headers": {
            "accept": "application/json, text/plain, */*",
            "accept-language": "en-US,en;q=0.9,ru;q=0.8",
            "authorization": token,
            "cache-control": "no-cache",
            "content-type": "application/json;charset=utf-8",
            "pragma": "no-cache",
            "route": "gbr/en/hun",
            "sec-ch-ua": "\"Google Chrome\";v=\"113\", \"Chromium\";v=\"113\", \"Not-A.Brand\";v=\"24\"",
            "sec-ch-ua-mobile": "?0",
            "sec-ch-ua-platform": "\"Windows\"",
            "sec-fetch-dest": "empty",
            "sec-fetch-mode": "cors",
            "sec-fetch-site": "same-site"
        },
        "referrer": "https://visa.vfsglobal.com/",
        "referrerPolicy": "strict-origin-when-cross-origin",
        "body": null,
        "method": "GET",
        "mode": "cors",
        "credentials": "include"
    });

    return await slots.text();
}

await slots(
    "maxonmail@list.ru"
    , "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiSW5kaXZpZHVhbCIsInVzZXJJZCI6IkdTR0ZHbzJGUzRTTmNvRlI2elFYOGpzK2dkTDVIMGtDamJ5VjZWTlUyVVU9IiwiZW1haWwiOiIwOHRuMXIxMjBKdFovdjJKaUJydWhGL1drTEVmT2IvNW9nYkRsVG5FZlhSc1hIK2tZTnBTMDI5RDJZRTVkSlNvIiwibmJmIjoxNjg0NTIyNTQ4LCJleHAiOjE2ODQ1Mjg1NDgsImlhdCI6MTY4NDUyMjU0OH0.hfvNXKY3EFe-MTjK1SCfKDN1ltOjxqD5eRWC9lPL4Mg"
)

//RESULT
/*
[
    {
        "mission": "Hungary",
        "center": "Hungary Visa Application Center - London",
        "visacategory": "TA",
        "date": "06/05/2023",
        "isWeekend": false,
        "counters": [
            {
                "allocationCategory": "Short Stay",
                "categoryCode": "CC & GU",
                "groups": [
                    {
                        "visaGroupName": "Short Stay",
                        "timeSlots": [
                            {
                                "allocationId": 21877139,
                                "timeSlot": "11:45-12:00",
                                "slotType": "Normal",
                                "totalSeats": 1,
                                "remainingSeats": 1,
                                "startTimetick": 0
                            }
                        ]
                    }
                ]
            }
        ],
        "error": null
    }
]*/



//=========================================

async function book(allocationId, email, urn, token) {
    let visa = await fetch("https://lift-api.vfsglobal.com/appointment/schedule", {
        "headers": {
            "accept": "application/json, text/plain, */*",
            "accept-language": "en-US,en;q=0.9,ru;q=0.8",
            "authorize": token,
            "cache-control": "no-cache",
            "content-type": "application/json;charset=UTF-8",
            "pragma": "no-cache",
            "route": "gbr/en/hun",
            "sec-ch-ua": "\"Google Chrome\";v=\"113\", \"Chromium\";v=\"113\", \"Not-A.Brand\";v=\"24\"",
            "sec-ch-ua-mobile": "?0",
            "sec-ch-ua-platform": "\"Windows\"",
            "sec-fetch-dest": "empty",
            "sec-fetch-mode": "cors",
            "sec-fetch-site": "same-site"
        },
        "referrer": "https://visa.vfsglobal.com/",
        "referrerPolicy": "strict-origin-when-cross-origin",
        "body": "{\"missionCode\":\"hun\",\"countryCode\":\"gbr\",\"centerCode\":\"HUN-LON\",\"loginUser\":\"" + email + "\",\"urn\":\"" + urn + "\",\"notificationType\":\"none\",\"paymentdetails\":{\"paymentmode\":\"Online\",\"RequestRefNo\":\"\",\"clientId\":\"\",\"merchantId\":\"\",\"amount\":29,\"currency\":\"GBP\"},\"allocationId\":\"" + allocationId + "\",\"CanVFSReachoutToApplicant\":false}",
        "method": "POST",
        "mode": "cors",
        "credentials": "omit"
    });
    return await visa.text();
}

await book(
    "21877138"
    , "maxonmail@list.ru"
    , "HUN71544859968"
    , "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJyb2xlIjoiSW5kaXZpZHVhbCIsInVzZXJJZCI6IkdTR0ZHbzJGUzRTTmNvRlI2elFYOGpzK2dkTDVIMGtDamJ5VjZWTlUyVVU9IiwiZW1haWwiOiIwOHRuMXIxMjBKdFovdjJKaUJydWhGL1drTEVmT2IvNW9nYkRsVG5FZlhSc1hIK2tZTnBTMDI5RDJZRTVkSlNvIiwibmJmIjoxNjg0NTIyNTQ4LCJleHAiOjE2ODQ1Mjg1NDgsImlhdCI6MTY4NDUyMjU0OH0.hfvNXKY3EFe-MTjK1SCfKDN1ltOjxqD5eRWC9lPL4Mg"
)

//RESULT
/*{
    "IsAppointmentBooked": true,
    "IsPaymentRequired": true,
    "RequestRefNo": 1008609109,
    "URL": "https://online.vfsglobal.com/PG-Component/Payment/PGRequest",
    "DigitalSignature": "ho_D1VeX6NY_VuL4Et8BvUGFZpvmtHD5Urogb8JXQGhvIXxCg7qiBNvVLAqypXqd58d5ouDSYV_n8GUBtc95kw,,",
    "error": null
}*/
//===================================


//https://online.vfsglobal.com/PG-Component/Payment/PGRequest?RequestRefNo=!!!&Email=!!!&Currency=GBP&Culture=en-US&Amount=29&DigitalSignature=!!!&RedirectURL=https://visa.vfsglobal.com/gbr/en/hun/payments/confirmappointment


//https://online.vfsglobal.com/PG-Component/Payment/PGRequest?RequestRefNo=1008609109&Email=maxonmail@list.ru&Currency=GBP&Culture=en-US&Amount=29&DigitalSignature=ho_D1VeX6NY_VuL4Et8BvUGFZpvmtHD5Urogb8JXQGhvIXxCg7qiBNvVLAqypXqd58d5ouDSYV_n8GUBtc95kw,,&RedirectURL=https://visa.vfsglobal.com/gbr/en/hun/payments/confirmappointment

//====================================

async function checkPayment(requestRefNo) {
    let result = await fetch("https://lift-api.vfsglobal.com/payments/trackpaymentstatus?RequestRefNo=" + requestRefNo);
    return await result.text();
}

await checkPayment("1008609109");

//RESULT
/*{
    "Status": "SUCCESS",
    "TransactionId": "5cf572dd-0a0c-4582-9e2f-ed1cddf7de69",
    "Amount": 29,
    "TransactionDate": "5/19/2023 7:24:56 PM"
}*/

//====================================

async function confirm(email, urn, bankReferenceNo, requestRefNo, token) {
    let visa = await fetch("https://lift-api.vfsglobal.com/payments/confirmappointment", {
        "headers": {
            "accept": "application/json, text/plain, */*",
            "accept-language": "en-US,en;q=0.9,ru;q=0.8",
            "authorize": token,
            "content-type": "application/json;charset=UTF-8",
            "pragma": "no-cache",
            "route": "gbr/en/hun",
            "sec-ch-ua": "\"Google Chrome\";v=\"113\", \"Chromium\";v=\"113\", \"Not-A.Brand\";v=\"24\"",
            "sec-ch-ua-mobile": "?0",
            "sec-ch-ua-platform": "\"Windows\"",
            "sec-fetch-dest": "empty",
            "sec-fetch-mode": "cors",
            "sec-fetch-site": "same-site"
        },
        "body": `{
        "missionCode": "hun",
        "countryCode": "gbr",
        "loginUser": "${email}",
        "urn": "${urn}",
        "bankReferenceNo": "${bankReferenceNo}",
        "requestReferenceNo": "${requestRefNo}",
        "amount": 29,
        "currency": "GPB",
        "cultureCode": "en-US",
        "centerCode": "HUN-LON"
    }`,
        "method": "POST",
        "mode": "cors",
        "credentials": "omit"
    });
    return await visa.text();
}