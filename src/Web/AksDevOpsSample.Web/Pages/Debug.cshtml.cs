using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AksDevOpsSample.Web.Pages
{
    [BindProperties]
    public class DebugModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        public string DnsQuery { get; set; }

        public string DnsQueryResult { get; set; }

        public string UrlQuery { get; set; }

        public string UrlQueryResult { get; set; }

        public DebugModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task OnPostUrlQueryAsync()
        {
            try
            {
                var httpClient = this.httpClientFactory.CreateClient();
                var content = await httpClient.GetStringAsync(this.UrlQuery);
                this.UrlQueryResult = content;
            }
            catch (Exception ex)
            {
                this.UrlQueryResult = ex.ToString();
            }
        }

        public void OnPostDnsQuery()
        {
            try
            {
                var result = new StringBuilder();
                var hostEntry = System.Net.Dns.GetHostEntry(this.DnsQuery);
                foreach (var ip in hostEntry.AddressList)
                {
                    if (result.Length > 0)
                        result.Append(',');

                    result.Append(ip.ToString());
                }

                this.DnsQueryResult = result.ToString();
            }
            catch (Exception ex)
            {
                this.DnsQueryResult = ex.ToString();
            }

        }
    }
}