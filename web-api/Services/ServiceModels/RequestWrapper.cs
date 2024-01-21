// Note that using too many wrappers for generisism can result 
// in bloated code and causes wrapper hell.

// For now a single string will do for error messages
public class RequestWrapper {
    public bool Success { get; set; }
    public string? Message { get; set; } 
}

public class RequestWrapper<T> {
    public T? Result { get; set; }
    public bool Success { get; set; }
    public string? Message { get; set; }
}