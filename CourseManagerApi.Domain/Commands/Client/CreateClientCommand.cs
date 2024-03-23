using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CourseManagerApi.Domain.Enums;
using CourseManagerApi.Domain.Extensions;
using CourseManagerApi.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace CourseManagerApi.Domain.Commands.Client;

#pragma warning disable CS8618
public class CreateClientCommand : Notifiable<Notification>, ICommand
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BadgeName { get; set; }
    public string Document { get; set; }
    public EDocumentType DocumentType { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }
    public EGenderType GenderType { get; set; }
    public string GenderDetail { get; set; }
    public string Street { get; set; }
    public string AddressNumber { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
    public string AddOnAddress { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsSmoker { get; set; }
    public string Observation { get; set; }

    public void Validate()
    {
        if (VerifyNullValuesToNotifications())
            AddNotifications(
                new Contract<CreateClientCommand>()
                    .Requires()
                    .IsTrue(Email.IsEmail(), "CreateClientCommand.Email", "Email em formato inválido")
                    .IsGreaterOrEqualsThan(FirstName.Length, 3, "CreateClientCommand.FirstName", "Primeiro nome deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(LastName.Length, 3, "CreateClientCommand.LastName", "Sobrenome deve conter 3 ou mais caracteres")
                    .IsLowerOrEqualsThan(FirstName.Length, 128, "CreateClientCommand.FirstName", "Primeiro nome deve conter 128 ou menos caracteres")
                    .IsLowerOrEqualsThan(LastName.Length, 128, "CreateClientCommand.LastName", "Sobrenome deve conter 128 ou menos caracteres")
                    .IsGreaterOrEqualsThan(BadgeName.Length, 3, "CreateClientCommand.BadgeName", "Nome do crachá deve conter 3 ou mais caracteres")
                    .IsLowerOrEqualsThan(BadgeName.Length, 32, "CreateClientCommand.BadgeName", "Nome do crachá deve conter 32 ou menos caracteres")
                    .IsTrue(DocumentType.Validate(Document), "CreateClientCommand.Document", "Número do documento inválido")
                    .AreEquals(AreaCode.Length, 2, "CreateClientCommand.AreaCode", "Código de área deve possuir 2 digitos")
                    .IsTrue(AreaCode.IsAreaCode(), "CreateClientCommand.AreaCode", "Formato do código de área inválido")
                    .AreEquals(PhoneNumber.Length, 9, "CreateClientCommand.PhoneNumber", "Número de celular deve possuir 9 dígitos")
                    .IsTrue(PhoneNumber.IsPhoneNumber(), "CreateClientCommand.PhoneNumber", "Formato do número de celular inválido")
                    .IsGreaterOrEqualsThan(Street.Length, 3, "CreateClientCommand.Street", "Rua deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(AddressNumber.Length, 1, "CreateClientCommand.AddressNumber", "Número do endereço deve conter 1 ou mais caracteres")
                    .IsGreaterOrEqualsThan(Neighborhood.Length, 3, "CreateClientCommand.Neighborhood", "Bairro deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(City.Length, 3, "CreateClientCommand.City", "Cidade deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(State.Length, 3, "CreateClientCommand.State", "Estado deve conter 3 ou mais caracteres")
                    .IsGreaterOrEqualsThan(Country.Length, 3, "AdCreateClientCommanddress.Country", "País deve conter 3 ou mais caracteres")
                    .IsLowerOrEqualsThan(Street.Length, 128, "CreateClientCommand.Street", "Rua deve conter 128 ou menos caracteres")
                    .IsLowerOrEqualsThan(AddressNumber.Length, 6, "CreateClientCommand.AddressNumber", "Número do endereço deve conter 6 ou menos caracteres")
                    .IsLowerOrEqualsThan(Neighborhood.Length, 32, "CreateClientCommand.Neighborhood", "Bairro deve conter 32 ou menos caracteres")
                    .IsLowerOrEqualsThan(City.Length, 32, "CreateClientCommand.City", "Cidade deve conter 32 ou menos caracteres")
                    .IsLowerOrEqualsThan(State.Length, 32, "CreateClientCommand.State", "Estado deve conter 32 ou menos caracteres")
                    .IsLowerOrEqualsThan(Country.Length, 32, "CreateClientCommand.Country", "País deve conter 32 ou menos caracteres")
                    .IsNotNull(BirthDate, "Client.BirthDate", "Data de nascimento deve ser informado")
            );

        if (!String.IsNullOrEmpty(GenderDetail))
            AddNotifications(
                new Contract<CreateClientCommand>()
                    .Requires()
                    .IsGreaterOrEqualsThan(GenderDetail.Length, 3, "CreateClientCommand.GenderDetail", "Detalhe do gênero deve conter 3 ou mais caracteres")
                    .IsLowerOrEqualsThan(GenderDetail.Length, 32, "CreateClientCommand.GenderDetail", "Detalhe do gênero deve conter 32 ou menos caracteres")
            );

        if (!String.IsNullOrEmpty(ZipCode))
            AddNotifications(
                new Contract<CreateClientCommand>()
                    .Requires()
                    .AreEquals(ZipCode.Length, 8, "CreateClientCommand.ZipCode","CEP inválido")
                    .IsTrue(Regex.IsMatch(ZipCode, "^[0-9]{8}$"), "CreateClientCommand.ZipCode", "Formato do CEP inválido")
            );

        if (!String.IsNullOrEmpty(AddOnAddress))
            AddNotifications(
                new Contract<CreateClientCommand>()
                    .Requires()
                    .IsGreaterOrEqualsThan(AddOnAddress.Length, 3, "CreateClientCommand.AddOnAddress", "Complemento do endereço deve conter 3 ou mais caracteres")
                    .IsLowerOrEqualsThan(AddOnAddress.Length, 32, "CreateClientCommand.AddOnAddress", "Complemento do endereço deve conter 32 ou menos caracteres")
            );

        if (!String.IsNullOrEmpty(Observation))
            AddNotifications(
                new Contract<CreateClientCommand>()
                    .Requires()
                    .IsLowerOrEqualsThan(Observation.Length, 256, "CreateClientCommand.Observation", "Observação deve ser menor ou igual a 256 caracteres")
            );
    }

    public bool VerifyNullValuesToNotifications()
    {
        AddNotifications(
            new Contract<CreateClientCommand>()
                .Requires()
                .IsNotNullOrEmpty(Email, "CreateClientCommand.Email", "Email não pode ser nulo")
                .IsNotNullOrEmpty(FirstName, "CreateClientCommand.FirstName", "Primeiro nome inválido")
                .IsNotNullOrEmpty(LastName, "CreateClientCommand.LastName", "Sobrenome inválido")
                .IsNotNullOrEmpty(BadgeName, "CreateClientCommand.LastName", "Sobrenome inválido")
                .IsNotNullOrEmpty(Document, "CreateClientCommand.Document", "Número do documento inválido")
                .IsNotNullOrEmpty(DocumentType.ToString(), "CreateClientCommand.DocumentType", "Tipo do documento inválido")
                .IsNotNullOrEmpty(AreaCode, "CreateClientCommand.AreaCode", "Código de área inválido")
                .IsNotNullOrEmpty(PhoneNumber, "CreateClientCommand.PhoneNumber", "Número de celular inválido")
                .IsNotNullOrEmpty(GenderType.ToString(), "CreateClientCommand.GenderType", "Gênero inválido")
                .IsNotNullOrEmpty(Street, "CreateClientCommand.Street", "Rua inválido")
                .IsNotNullOrEmpty(AddressNumber, "CreateClientCommand.AddressNumber", "Número do endereço inválido")
                .IsNotNullOrEmpty(Neighborhood, "CreateClientCommand.Neighborhood", "Bairro inválido")
                .IsNotNullOrEmpty(City, "CreateClientCommand.City", "Cidade inválido")
                .IsNotNullOrEmpty(State, "CreateClientCommand.State", "Estado inválido")
                .IsNotNullOrEmpty(Country, "CreateClientCommand.Country", "País inválido")
        );

        if (GenderType.IsOther())
            AddNotifications(
                new Contract<CreateClientCommand>()
                    .Requires()
                    .IsNotNullOrEmpty(GenderDetail, "CreateClientCommand.GenderDetail", "Gênero deve ser especificado")
            );

        return IsValid;
    }
}