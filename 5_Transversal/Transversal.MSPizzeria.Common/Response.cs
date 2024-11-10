using FluentValidation.Results;

namespace Transversal.MSPizzeria.Common;

public class Response<T>
{
    public Response()
    {
        Error = new List<ValidationFailure>();
        ErrorMessage = new List<string>();
    }
    
    public T Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    
    public IEnumerable<ValidationFailure> Error { get; set; }
    public List<string> ErrorMessage { get; set; }
}