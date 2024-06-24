using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Contexts.SharedContext.Entities;

namespace CourseManagerApi.Core.Contexts.ClassContext.Entities;

public class Contract : Entity
{
    protected Contract() { }
    public Contract(Class classEntity, Client client)
    {
        Class = classEntity;
        Client = client;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Class Class { get; private set; } = null!;
    public Client Client { get; private set; } = null!;
    public Payment? Payment { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public Tenant Tenant { get; private set; } = null!;

    public void SetTenant(Tenant tenant)
    {
        if (Tenant == null)
            Tenant = tenant;
    }

    public void SetPayment(Payment payment) => Payment = payment;
}