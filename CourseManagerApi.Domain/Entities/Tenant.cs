using CourseManagerApi.Shared.Entities;
using Flunt.Validations;

namespace CourseManagerApi.Domain.Entities;

public class Tenant : Entity
{
    public Tenant(string name)
    {
        Name = name;

        VerifyNotifications();
    }

    public string Name { get; private set; }

    protected override void VerifyNotifications()
    {
        if (String.IsNullOrEmpty(Name))
            AddNotification("Tenant.Name", "Nome inválido");

        AddNotifications(
            new Contract<Tenant>()
                .Requires()
                .IsGreaterOrEqualsThan(Name.Length, 2, "Tenant.Name", "Nome deve conter 3 ou mais caracteres")
                .IsLowerOrEqualsThan(Name.Length, 128, "Tenant.Name", "Nome deve conter 128 ou menos caracteres")
        );
    }
}