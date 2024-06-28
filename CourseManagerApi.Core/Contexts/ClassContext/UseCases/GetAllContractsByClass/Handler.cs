using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllContractsByClass.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.ClassContext.UseCases.GetAllContractsByClass;

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

        List<Contract> contracts;

        try
        {
            contracts = await _repository.GetContractsByClassIdAsync(request.Id, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível recuperar os dados", 500);
        }

        #endregion

        #region 03. Retorna os dados

        try
        {
            var data = contracts
                .Select(x => {
                    PaymentResponseData? payment;

                    if (x.Payment != null)
                    {
                        List<InstallmentResponseData> installments = new();

                        if (x.Payment.Installments.Any())
                            installments.AddRange(x.Payment.Installments.Select(x => new InstallmentResponseData(x.Id.ToString(), x.Money, (int)x.PaymentStatus, (int)x.PaymentMethod, x.DueDate)));

                        payment = new PaymentResponseData(x.Payment.Id.ToString(), installments);
                    }
                    else payment = null;

                    return new ResponseData(x.Id.ToString(), x.Client.Id.ToString(), x.Client.Name, x.Client.Email, payment);
                });

            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Não foi possível obter os dados da turma", 500);
        }

        #endregion
    }
}