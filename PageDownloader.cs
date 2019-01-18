using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class PageDownloader
{
    private HttpClient _client;
    private Encoding _encoding;

    public PageDownloader()
        : this(Encoding.UTF8) { }

    public PageDownloader(Encoding encoding)
    {
        _encoding = encoding;
        _client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
    }

    public async Task<string> GetPageData(string link)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, link);
        request.Headers.Add("User-Agent", "Chrome/21.0.1180.89");
        request.Headers.Add("Accept", "text/html");

        HttpResponseMessage response = await _client.GetAsync(request.RequestUri);

        return await response.Content.ReadAsStringAsync(); ;
    }
}