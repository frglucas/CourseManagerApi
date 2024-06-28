using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment.Contracts;
using MediatR;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.PayInstallment;

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

        #region 02. Recupera os objetos

        Installment? installment;

        try
        {
            installment = await _repository.FindInstallmentByIdAsync(request.InstallmentId, cancellationToken);
            if (installment == null)
                return new Response("Não foi possível encontrar a parcela", 404);

        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion

        #region 03. Validações

        try
        {
            if (installment.Payment.Id.ToString().ToUpper() != request.PaymentId.ToUpper())
                return new Response("Esta parcela não pertence ao pagamento informado", 400);

            if (installment.PaymentStatus == Enums.EPaymentStatus.Settled)
                return new Response("Esta parcela já foi paga", 400);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 500);
        }

        #endregion

        #region 04. Modifica objeto

        try
        {
            installment.SetPaymentStatus(Enums.EPaymentStatus.Settled);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 500);
        }

        #endregion

        #region 05. Persistir dados

        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            return new Response("Erro ao persistir dados", 500);
        }

        #endregion

        return new Response("Parcela paga com sucesso", new ResponseData(installment.Id));
    }
}