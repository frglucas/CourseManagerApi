using CourseManagerApi.Core.Contexts.ClassContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.Entities;
using CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment.Contracts;
using CourseManagerApi.Core.Contexts.TenantContext.Entities;
using CourseManagerApi.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CourseManagerApi.Core.Contexts.PaymentContext.UseCases.AddInstallment;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IHttpContextAccessor _contextAccessor;

    public Handler(IRepository repository, IHttpContextAccessor contextAccessor)
    {
        _repository = repository;
        _contextAccessor = contextAccessor;
    }

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

        Tenant? tenant;
        Contract? contract;

        try
        {
            var tenantId = _contextAccessor.HttpContext.User.TenantId();
            tenant = await _repository.FindTenantByIdAsync(tenantId, cancellationToken);
            if (tenant == null)
                return new Response("Não foi possível encontrar o tenant", 404);

            contract = await _repository.FindContractByIdAsync(request.ContractId, cancellationToken);
            if (contract == null)
                return new Response("Não foi possível encontrar o contrato", 404);

        }
        catch
        {
            return new Response("Não foi possivel buscar informações necessárias", 500);
        }

        #endregion

        #region 03. Validações

        // Any

        #endregion

        #region 04. Gerar objetos
        
        Installment installment;
        Payment? payment;

        try
        {
            payment = contract.Payment;
            if (payment == null) 
            {
                payment = new Payment(request.Money, 1, Enums.EPaymentStatus.NotPaid);  
                payment.SetContract(contract);
                payment.SetTenant(tenant);
            } 
            else
            {
                payment.SumTotalPrince(request.Money);
            } 

            installment = new Installment(request.Money, request.DueDate);
            installment.SetPayment(payment); 
            installment.SetTenant(tenant);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        #endregion

        #region 05. Persistir dados

        try
        {
            if (contract.Payment == null)
                await _repository.SaveAsync(payment, cancellationToken);

            await _repository.SaveAsync(installment, cancellationToken);
        }
        catch
        {
            return new Response("Erro ao persistir dados", 500);
        }

        #endregion

        return new Response("Parcela adicionada com sucesso", new ResponseData(installment.Id));
    }
}