using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllByNameAndPaged;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsGreaterThan(request.page, 0, "Page", "A página informada deve ser maior que zero")
            .IsGreaterThan(request.pageSize, 0, "PageSize", "O tamanho da página informado deve ser maior que zero");
}