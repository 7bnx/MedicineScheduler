using System.ComponentModel.DataAnnotations;

namespace MedicineScheduler.Common;

public interface IOperationResult
{
  string? Message { get; init; }
  Exception? Exception { get; init; }
  bool IsOk { get; }
}

public class OperationResult<TResult> : IOperationResult
{
  public TResult? Value { get; init; }
  public string? Message { get; init; }
  public Exception? Exception { get; init; }
  public OperationResult(IOperationResult src, TResult result = default!) : this(result, src.Message, src.Exception){ }
  public OperationResult(TResult? result, string? message = null, Exception? exception = null)
    => (Value, Message, Exception) = (result, message, exception);
  public bool IsOk
    => Exception is null;
  public OperationResult(string? message) : this(default, message, null) { }
  public OperationResult(Exception? exception) : this(default, null, exception) { }
  public OperationResult(string? message, Exception? exception) : this(default, message, exception) { }
  public OperationResult(IEnumerable<ValidationResult>? validationResults, string? message = null)
  {
    Exception = new ValidationException(message);
    var validationMessage = validationResults?.Select((x, i) => $"{i + 1}. Message: {x.ErrorMessage}; Members: {string.Join(',', x.MemberNames)}");
    Message = string.Join(Environment.NewLine, validationMessage!);
  }
}
