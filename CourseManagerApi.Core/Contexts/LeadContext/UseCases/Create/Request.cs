using MediatR;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Create;

public record Request(
    string FullName,
    string Email = "", 
    string AreaCode = "",
    string PhoneNumber = "",  
    string Observation = ""
) : IRequest<Response>;