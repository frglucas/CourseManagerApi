using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.CourseContext.UseCases.Edit;

public static class Specification 
{
    public static Contract<Notification> Ensure(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.CourseId, "CourseId", "Curso não especificado")
            .IsLowerThan(request.Name.Length, 257, "Name", "O nome deve conter menos de 257 caracteres")
            .IsGreaterThan(request.Name.Length, 1, "Name", "O nome deve conter mais de 1 caracter")
            .IsLowerThan(request.Description.Length, 513, "Description", "A descrição deve conter menos de 257 caracteres")
            .IsGreaterThan(request.Description.Length, 1, "Description", "A descrição deve conter mais de 1 caracter");
}