using CourseManagerApi.Shared.Commands;

namespace CourseManagerApi.Domain.Commands;

public class CommandResult : ICommandResult
{
    public CommandResult(bool success, string message, object data = null!)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
}