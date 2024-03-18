using CourseManagerApi.Domain.ValueObjects;
using CourseManagerApi.Shared.Entities;

namespace CourseManagerApi.Domain.Entities;

public class User : Entity
{
    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Gender Gender { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Occupation { get; private set; }
    public bool IsSmoker { get; private set; }
    public string Observation { get; private set; }
    public string Street { get; private set; }
    public string AddressNumber { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    public string AddOnAddress { get; private set; }
}