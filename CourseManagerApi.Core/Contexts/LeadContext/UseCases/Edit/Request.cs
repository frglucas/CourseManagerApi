using MediatR;

namespace CourseManagerApi.Core.Contexts.LeadContext.UseCases.Edit;

public record Request(
    string LeadId,
    string FullName,
    string Email = "", 
    string AreaCode = "",
    string PhoneNumber = "",  
    string Observation = ""
) : IRequest<Response>;