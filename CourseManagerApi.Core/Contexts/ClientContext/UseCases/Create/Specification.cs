using CourseManagerApi.Core.Extensions;
using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido")
            .IsLowerThan(request.FullName.Length, 256, "FullName", "O nome deve conter menos de 256 caracteres")
            .IsGreaterThan(request.FullName.Length, 2, "FullName", "O nome deve conter mais de 2 caracteres")
            .IsLowerThan(request.Document.Length, 15, "Document", "O documento deve conter menos de 15 caracteres")
            .IsGreaterThan(request.Document.Length, 10, "Document", "O documento deve conter mais de 10 caracteres")
            .IsNotNull(request.CaptivatorId, "CaptivatorId", "O captador deve ser informado")
            .IsNotNull(request.DocumentType, "DocumentType", "O tipo do documento deve ser especificado")
            .IsNotNull(request.GenderType, "GenderType", "O gênero deve ser especificado")
            .IsLowerThan(request.GenderDetail.Length, 50, "GenderDetail", "O detalhe do gênero deve conter menos de 50 caracteres")
            .IsNotNull(request.BirthDate, "BirthDate", "A data de nascimento deve ser informada")
            .IsLowerThan(request.Observation.Length, 512, "Observation", "A observação deve conter menos de 512 caracteres")
            .IsNotNull(request.IsSmoker, "IsSmoker", "Necessário informar se é fumante");

    public static void EnsurePhoneNumbers(this Contract<Notification> contract, PhoneNumberRequest phoneNumberRequest)
    {
        contract
            .IsNotNull(phoneNumberRequest.AreaCode, "AreaCode", $"O código de área {phoneNumberRequest.AreaCode} informado é inválido")
            .IsNotNullOrEmpty(phoneNumberRequest.AreaCode, "AreaCode", $"O código de área {phoneNumberRequest.AreaCode} informado é inválido")
            .IsNotNull(phoneNumberRequest.PhoneNumber, "PhoneNumber", $"O número {phoneNumberRequest.PhoneNumber} informado é inválido")
            .IsNotNullOrEmpty(phoneNumberRequest.PhoneNumber, "PhoneNumber", $"O número {phoneNumberRequest.PhoneNumber} informado é inválido");

        if (contract.IsValid)
            contract
                .IsTrue(phoneNumberRequest.AreaCode.IsAreaCode(), "AreaCode", $"O código de área {phoneNumberRequest.AreaCode} informado é inválido")
                .IsTrue(phoneNumberRequest.PhoneNumber.IsPhoneNumber(), "PhoneNumber", $"O número {phoneNumberRequest.PhoneNumber} informado é inválido");
    }
}
