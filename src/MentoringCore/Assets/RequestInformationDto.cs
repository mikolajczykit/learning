using Microsoft.AspNetCore.Http;

namespace MentoringCore.Assets
{
    public class RequestInformationDto
    {
        public RequestInformationDto(HttpContext context) 
        {
            Method = context.Request.Method;
            Path = context.Request.Path;
            ResponseCode = context.Response.StatusCode;
        }

        public string Method { get; private set; }
        public PathString Path { get; private set; }
        public int ResponseCode { get; private set; }
    }
}
