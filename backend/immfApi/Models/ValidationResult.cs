namespace immfApi.Models
{
    public class ValidationResult<T>
    {
        public T? Value { get; init; }
        public string? Error { get; init; }
        public bool IsSuccess { get; init; }

        public static ValidationResult<T> Success(T value) => new() { IsSuccess = true, Value = value };
        public static ValidationResult<T> Fail(string error) => new() { IsSuccess = false, Error = error };
    }
}