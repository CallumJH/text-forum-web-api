// Note that using too many wrappers for generisism can result 
// in bloated code and causes wrapper hell.

// For now a single string will do for error messages
public class RequestWrapper {
    public bool Success { get; set; } = false;
    public string? Message { get; set; } = "Something went wrong";
    public RequestWrapper SetSucceeded(string message = "Success") {
        Success = true;
        Message = message;
        return this;
    }

    public RequestWrapper SetFailed(string message = "Something went wrong") {
        Success = false;
        Message = message;
        return this;
    }
}

public class RequestWrapper<T> {
    public T? Data { get; set; }
    public bool Success { get; set; } = false;
    public string? Message { get; set; } = "Something went wrong";
    public RequestWrapper<T> SetSucceeded( T? data = default, string message = "Success") {
        Success = true;
        Message = message;
        Data = data;
        return this;
    }

    public RequestWrapper<T> SetFailed(string message = "Something went wrong") {
        Success = false;
        Message = message;
        return this;
    }
}