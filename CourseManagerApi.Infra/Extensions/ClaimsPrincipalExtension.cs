using System.Security.Claims;

namespace CourseManagerApi.Infra.Extensions;

public static class ClaimsPrincipalExtension 
{
    public static string TenantId(this ClaimsPrincipal user) 
        => user.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value ?? string.Empty;
}