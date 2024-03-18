using CourseManagerApi.Domain.Enums;
using CourseManagerApi.Shared.ValueObjects;
using Flunt.Validations;

namespace CourseManagerApi.Domain.ValueObjects;

public class Gender : ValueObject
{
    public Gender(EGenderType genderType, string genderDetail = "")
    {
        GenderType = genderType;
        GenderDetail = genderType.Equals(EGenderType.Other) ? genderDetail : String.Empty;

        VerifyNotifications();
    }

    public EGenderType GenderType { get; private set; }
    public string GenderDetail { get; private set; }

    private bool GenderTypeIsOther() => GenderType.Equals(EGenderType.Other);

    public override bool Validate()
    {
        VerifyNotifications();
        return IsValid;
    }

    protected override void VerifyNotifications()
    {
        if (VerifyNullValuesToNotifications() && GenderTypeIsOther())
            AddNotifications(
                new Contract<Gender>()
                    .Requires()
                    .IsGreaterOrEqualsThan(GenderDetail.Length, 3, "Gender.GenderDetail", "Detalhe do gênero deve conter 3 ou mais caracteres")
                    .IsLowerOrEqualsThan(GenderDetail.Length, 32, "Gender.GenderDetail", "Detalhe do gênero deve conter 32 ou menos caracteres")
            );
    }

    protected override bool VerifyNullValuesToNotifications()
    {
        AddNotifications(
            new Contract<Gender>()
                .Requires()
                .IsNotNullOrEmpty(GenderType.ToString(), "Gender.GenderType", "Gênero inválido")
        );

        if (GenderTypeIsOther())
            AddNotifications(
                new Contract<Gender>()
                    .Requires()
                    .IsNotNullOrEmpty(GenderDetail, "Gender.GenderDetail", "Gênero deve ser especificado")
            );

        return IsValid;
    }
}