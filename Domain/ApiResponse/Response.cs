using System.Net;

namespace Domain.ApiResponse;

public class Response<T>
{
    public bool IsSuccess { get; set; }
    public string Messenge { get; set; }
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    public Response()
    {

    }
    public Response(T data, string? messenge = null)
    {
        IsSuccess = true;
        Messenge = messenge;
        Data = data;
        StatusCode = (int)HttpStatusCode.OK;
    }
    public Response(HttpStatusCode statusCode, string? messenge = null)
    {
        IsSuccess = false;
        Messenge = messenge;
        Data = default;
        StatusCode = (int)statusCode;
    }
}
