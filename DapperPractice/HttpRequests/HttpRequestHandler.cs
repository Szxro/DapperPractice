using DapperPractice.Repositories.MoviesRepository;

namespace DapperPractice.HttpRequests
{
    public class HttpRequestHandler : DelegatingHandler
    {
        //Can use ILogger to see what is happening
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //This will handle the request that send the HttpClient in the desired class that was put in the program.cs
            var response = await base.SendAsync(request, cancellationToken);
            //is going to return the response
            return response;
        }
    }
}
