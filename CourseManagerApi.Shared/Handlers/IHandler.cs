using CourseManagerApi.Shared.Commands;

namespace CourseManagerApi.Shared.Handlers;

public interface IHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}