using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.Edit;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.ClassId, "ClassId", "Turma não especificada")
            .IsNotNull(request.CourseId, "CourseId", "O curso deve ser especificado")
            .IsNotNull(request.MinisterId, "Minister", "O ministrante deve ser especificado")
            .IsGreaterThan(request.Name.Length, 0, "Name", "O nome deve contem 1 caracter ou mais")
            .IsLowerOrEqualsThan(request.Name.Length, 256, "Name", "O nome deve contem 256 caracteres ou menos")
            .IsGreaterThan(request.AddressOrLink.Length, 0, "AddressOrLink", "O local ou link deve contem 1 caracter ou mais")
            .IsLowerOrEqualsThan(request.AddressOrLink.Length, 512, "AddressOrLink", "O local ou link deve contem 512 caracteres ou menos")
            .IsNotNull(request.ScheduledDate, "ScheduleDate", "A data que ocorrerá deve ser informada")
            .IsNotNull(request.IsOnline, "IsOnline", "Deve ser especificado se a turma será online");
}