using CourseManagerApi.Core.Extensions;
using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido")
            .IsTrue(request.AreaCode.IsAreaCode(), "AreaCode", "Código de área inválido")
            .IsTrue(request.PhoneNumber.IsPhoneNumber(), "PhoneNumber", "Número inválido")
            .IsLowerThan(request.FullName.Length, 256, "FullName", "O nome deve conter menos de 256 caracteres")
            .IsGreaterThan(request.FullName.Length, 2, "FullName", "O nome deve conter mais de 2 caracteres")
            .IsLowerThan(request.Observation.Length, 256, "Observation", "A observação deve conter menos de 512 caracteres");
}