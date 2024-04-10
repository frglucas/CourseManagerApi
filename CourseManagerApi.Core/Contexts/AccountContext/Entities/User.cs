using CourseManagerApi.Core.Contexts.AccountContext.ValueObjects;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.AccountContext.Entities;

public class User : Entity
{
    protected User()
    {
    }

    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public User(string email, string? password = null)
    {
        Email = email;
        Password = new Password(password);
    }
    
    public Name Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    public List<Role> Roles { get; set; } = new();

    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (!string.Equals(code.Trim(), Password.ResetCode.Trim(), StringComparison.CurrentCultureIgnoreCase))
            throw new Exception("Código de restauração inválido");

        ChangePassword(plainTextPassword);
    }

    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }
}