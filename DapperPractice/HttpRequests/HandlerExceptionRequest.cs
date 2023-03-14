namespace DapperPractice.HttpRequests
{
    public class HandlerExceptionRequest : DelegatingHandler
    {
        //Can put ILogger to see the error
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //This is a try/catch to handle exception that is throw
            try
            {
                return await base.SendAsync(request, cancellationToken);
            }
            catch(Exception ex)
            {
                //can put a Logger to see the error
                throw; //Throwing the error to the global handler
            }
        }
    }
}
