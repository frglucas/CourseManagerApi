using CourseManagerApi.Domain.ValueObjects;

namespace CourseManagerApi.Tests.ValueObjects;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [TestCategory("ValueObjects - Email")]
    [DataRow("teste@mail")]
    [DataRow("teste@mail.")]
    [DataRow("testemail.com")]
    [DataRow("@mail.com")]
    [DataRow("testemailcom")]
    public void ShouldReturnErrorWhenInvalidEmail(string value)
    {
        var email = new Email(value);

        Assert.IsFalse(email.IsValid);
    }
    
    [TestMethod]
    [TestCategory("ValueObjects - Email")]
    [DataRow("teste@mail.com")]
    [DataRow("teste@mail.com.br")]
    [DataRow("teste@gmail.com")]
    [DataRow("teste@hotmail.com")]
    [DataRow("teste123456@mail.com")]
    [DataRow("Teste@mail.com")]
    public void ShouldReturnErrorWhenValidEmail(string value)
    {
        var email = new Email(value);

        Assert.IsTrue(email.IsValid);
    }
}