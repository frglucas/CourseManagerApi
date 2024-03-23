using CourseManagerApi.Shared.Commands;

namespace CourseManagerApi.Shared.Handlers;

public interface IHandler<T> where T : ICommand
{
    Task<ICommandResult> Handle(T command, int tenantId);
}