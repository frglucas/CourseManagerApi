using CourseManagerApi.Domain.ValueObjects;
using CourseManagerApi.Shared.Entities;

namespace CourseManagerApi.Domain.Entities;

public class User : Entity
{
    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Gender Gender { get; private set; }
    public Address Address { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Occupation { get; private set; }
    public bool IsSmoker { get; private set; }
    public string Observation { get; private set; }
}