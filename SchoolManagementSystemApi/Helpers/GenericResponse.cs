using System.Net;

namespace SchoolManagementSystemApi.Helpers
{
    public class GenericResponse<T>
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
