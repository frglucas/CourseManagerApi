using CourseManagerApi.Core.Contexts.AccountContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.Entities;
using CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit.Contracts;
using CourseManagerApi.Core.Contexts.ClientContext.ValueObjects;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClientContext.UseCases.Edit;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Valida a requisição

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Requisição inválida", 400, res.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion

        #region 02. Recupera objetos

        Client? client;
        Occupation? occupation;
        User? captivator;
        Client? indicator;

        try
        {
            client = await _repository.FindClientByIdAsync(request.ClientId, cancellationToken);  
            if (client == null)
                return new Response("Não encontramos o cliente especificado", 404);

            if (request.OccupationId == null) occupation = null;
            else {
                occupation = await _repository.FindOccupationByIdAsync(request.OccupationId, cancellationToken);
                if (occupation == null)
                    return new Response("Não encontramos a ocupação profissional informada", 404);
            }

            captivator = await _repository.FindCaptivatorByIdAsync(request.CaptivatorId, cancellationToken);
            if (captivator == null)
                return new Response("Não foi possível encontrar o captador", 404);

            if (request.IndicatorIsCaptivator) indicator = null;
            else 
            {
                indicator = await _repository.FindIndicatorByIdAsync(request.IndicatorId, cancellationToken);
                if (indicator == null)
                    return new Response("Não foi possível encontrar o indicador", 404);
            }
        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion
    
        #region 03. Gerar objetos

        Document document;
        Email email;
        Gender gender;
        Name name;
        DateTime birthDate;

        try
        {
            document = new Document(request.Document, request.DocumentType);
            email = new Email(request.Email);
            gender = new Gender(request.GenderType, request.GenderDetail);
            
            if (string.IsNullOrEmpty(request.BadgeName) || string.IsNullOrWhiteSpace(request.BadgeName))
                name = new Name(request.FullName, request.FullName.Split(" ").First());
            else name = new Name(request.FullName, request.BadgeName);

            if (request.DocumentType == Enums.EDocumentType.CPF)
                birthDate = request.BirthDate;
            else birthDate = DateTime.MinValue;
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }

        #endregion
    
        #region 04. Validações
        
        try
        {
            var documentExists = await _repository.AnyByDocumentAsync(document.HashedNumber, cancellationToken);
            if (documentExists && (client.Document.HashedNumber != document.HashedNumber))
                return new Response($"Este {document.Type.ToString()} já está em uso", 400);
            
            var emailExists = await _repository.AnyByEmailAsync(email.Address, cancellationToken);
            if (emailExists && (client.Email.Address != email.Address))
                return new Response("Este E-mail já está em uso", 400);
        }
        catch
        {
            return new Response("Falha ao verificar documento e email cadastrados", 500);
        }
        
        #endregion

        #region 05. Adiciona valores ao cliente

        try
        {
            client.SetEmail(email);
            client.SetName(name);
            client.SetDocument(document);
            client.SetBirthDate(request.BirthDate);
            client.SetOccupation(occupation);
            client.SetIsSmoker(request.IsSmoker);
            client.SetGender(gender);
            client.SetObservation(request.Observation);
            client.SetNewUpdateAt();
            client.SetCaptivator(captivator);
            client.SetIndicator(indicator);
        }
        catch
        {
            return new Response("Erro ao inserir dados no cliente", 500);
        }

        #endregion

        #region 06. Persiste dados

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return new Response("Erro ao persistir dados", 500);
        }

        #endregion
    
        return new Response("Cliente editado", new ResponseData(client.Id));
    }
}