using CourseManagerApi.Shared.Entities;
using Flunt.Validations;

namespace CourseManagerApi.Domain.Entities;

public class Occupation : Entity
{
    public Occupation(int code, string description)
    {
        Code = code;
        Description = description;

        VerifyNotifications();
    }

    public int Code { get; private set; }
    public string Description { get; private set; }

    protected override void VerifyNotifications()
    {
        if (Code < 0)
            AddNotification("Occupation.Code", "Code inválido");

        if (String.IsNullOrEmpty(Description)) 
            AddNotification("Occupation.Description", "Descrição inválida");

        AddNotifications(
            new Contract<Occupation>()
                .Requires()
                .IsGreaterOrEqualsThan(Description.Length, 3, "Occupation.Description", "Descrição deve conter 3 ou mais caracteres")
                .IsLowerOrEqualsThan(Description.Length, 32, "Occupation.Description", "Descrição deve conter 32 ou menos caracteres")
        );
    }
}