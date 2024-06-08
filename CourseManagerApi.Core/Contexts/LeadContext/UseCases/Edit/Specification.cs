using CourseManagerApi.Core.Extensions;
using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNull(request.LeadId, "LeadId", "Id não informado")
            .IsLowerThan(request.FullName.Length, 256, "FullName", "O nome deve conter menos de 256 caracteres")
            .IsGreaterThan(request.FullName.Length, 2, "FullName", "O nome deve conter mais de 2 caracteres")
            .IsLowerThan(request.Observation.Length, 256, "Observation", "A observação deve conter menos de 512 caracteres");
}