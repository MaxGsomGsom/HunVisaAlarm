using System.Diagnostics;

while (true)
{
var uriBuilder = new UriBuilder("https://uk.blsspainvisa.com/visa4spain/book-date/YKamo5qiig");
var httpClient = new HttpClient();
var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
httpRequestMessage.Headers.Add("Host","uk.blsspainvisa.com");
httpRequestMessage.Headers.Add("Connection","keep-alive");
httpRequestMessage.Headers.Add("Cache-Control","max-age=0");
httpRequestMessage.Headers.Add("Upgrade-Insecure-Requests","1");
httpRequestMessage.Headers.Add("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36");
httpRequestMessage.Headers.Add("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
httpRequestMessage.Headers.Add("Sec-Fetch-Site","same-origin");
httpRequestMessage.Headers.Add("Sec-Fetch-Mode","navigate");
httpRequestMessage.Headers.Add("Sec-Fetch-User","?1");
httpRequestMessage.Headers.Add("Sec-Fetch-Dest","document");
httpRequestMessage.Headers.Add("Referer","https://uk.blsspainvisa.com/visa4spain/book-appointment/YKamo5qiig");
httpRequestMessage.Headers.Add("Accept-Encoding","gzip, deflate, br");
httpRequestMessage.Headers.Add("Accept-Language","en-US,en;q=0.9,ru;q=0.8");
httpRequestMessage.Headers.Add("Cookie","PHPSESSID=hpvhact926qk7nd0e0nr1qqb77; AWSALB=RGjfDz7Dz586wGnme35EpiNamBLcaZbSbbimZcPiPGcSu4FFmRSMlb5l/4RrjJyy/74o2HVYHcFVq64eoAekmrUSiNTz+34Vu4KphKFjoNRZ+6LEApUK0nr2+iq7; AWSALBCORS=RGjfDz7Dz586wGnme35EpiNamBLcaZbSbbimZcPiPGcSu4FFmRSMlb5l/4RrjJyy/74o2HVYHcFVq64eoAekmrUSiNTz+34Vu4KphKFjoNRZ+6LEApUK0nr2+iq7");
var httpResponseMessage = httpClient.SendAsync(httpRequestMessage).Result;
string result = httpResponseMessage.Content.ReadAsStringAsync().Result;
    if (!result.Contains("var available_dates = [];"))
    {
        Console.Beep(1000, 10000);
        Console.ReadLine();
    }

    Thread.Sleep(10000);
}