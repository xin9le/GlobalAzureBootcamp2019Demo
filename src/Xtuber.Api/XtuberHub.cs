using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;



namespace Xtuber.Api
{
    public static class XtuberHub
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Negotiate
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "xTuber")] SignalRConnectionInfo info
        ) => info;


        [FunctionName("broadcast")]
        public static async Task BroadcastAsync
        (
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalR(HubName = "xTuber")] IAsyncCollector<SignalRMessage> messages
        )
        {
            var data = await req.ReadAsStringAsync();
            await messages.AddAsync(new SignalRMessage
            {
                Target = "Receive",
                Arguments = new[] { data },
            });
        }
    }
}
