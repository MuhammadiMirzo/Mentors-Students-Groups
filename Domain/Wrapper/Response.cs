using System.Net;

namespace Domain.Wrapper;

public class Response<T>
{
   public T Data { get; set; }
   public string Message { get; set; }
   public HttpStatusCode StatusCode { get; set; }
   public Response(T data)
   {
    Data = data;
    StatusCode = HttpStatusCode.OK;
    Message = null; 
   }
   public Response(HttpStatusCode statusCode, string message)
   {
    StatusCode = statusCode;
    Message = message;
   }
}
