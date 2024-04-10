using System.Security.Claims;

namespace CourseManagerApi.Api.Extensions;

public static class ClaimsPrincipalExtension 
{
    public static string Id(this ClaimsPrincipal user) 
        => user.Claims.FirstOrDefault(c => c.Type == "id")?.Value ?? string.Empty;
    
    public static string Name(this ClaimsPrincipal user) 
        => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
    
    public static string Email(this ClaimsPrincipal user) 
        => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;

    public static string TenantId(this ClaimsPrincipal user) 
        => user.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value ?? string.Empty;
}