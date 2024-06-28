using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetContractById;

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

        Contract? contract;

        try
        {
            contract = await _repository.GetContractByIdAsync(request.Id, cancellationToken);
            if (contract == null)
                return new Response("Não foi possivel encontrar o contrato", 400);
        }
        catch
        {
            return new Response("Não foi possível recuperar os dados", 500);
        }

        #endregion

        #region 03. Retorna os dados

        try
        {

            PaymentResponseData? payment;
            List<InstallmentResponseData> installments = new();

            installments.AddRange(contract.Payment.Installments.Select(x => new InstallmentResponseData(x.Id.ToString(), x.Money, (int)x.PaymentStatus, (int)x.PaymentMethod, x.DueDate)));

            payment = new PaymentResponseData(contract.Payment.Id.ToString(), installments);

            var responseData = new ResponseData(contract.Id.ToString(), contract.Client.Id.ToString(), contract.Client.Name, contract.Client.Email, payment);

            return new Response("", responseData);
        }
        catch
        {
            return new Response("Não foi possível obter os dados da turma", 500);
        }

        #endregion
    }
}