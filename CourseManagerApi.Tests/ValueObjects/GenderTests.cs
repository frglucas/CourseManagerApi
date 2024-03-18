using CourseManagerApi.Domain.Enums;
using CourseManagerApi.Domain.ValueObjects;

namespace CourseManagerApi.Tests.ValueObjects;

[TestClass]
public class GenderTests
{
    public static readonly string stringGreater128 = new string('a', 129);

    [TestMethod]
    [TestCategory("ValueObjects - Gender")]
    [DataRow("")]
    [DataRow("ab")]
    [DataRow("abc", true)]
    public void ShouldReturnErrorWhenSetOtherAndSetInvalidDetail(string genderDetail, bool useStringGreater128 = false)
    {
        var value = useStringGreater128 ? stringGreater128 : genderDetail;

        var gender = new Gender(EGenderType.Other, value);

        Assert.IsFalse(gender.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Gender")]
    public void ShouldReturnSuccessWhenSetOtherAndSetDetail()
    {
        var gender = new Gender(EGenderType.Other, "Other");

        Assert.IsTrue(gender.Validate());
    }

    [TestMethod]
    [TestCategory("ValueObjects - Gender")]
    [DataRow(EGenderType.Uninformed)]
    [DataRow(EGenderType.Masculine)]
    [DataRow(EGenderType.Feminine)]
    public void ShouldReturnSuccess(EGenderType genderType)
    {
        var gender = new Gender(genderType);

        Assert.IsTrue(gender.Validate());
    }
}