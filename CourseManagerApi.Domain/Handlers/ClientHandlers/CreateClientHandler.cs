using CourseManagerApi.Domain.Commands;
using CourseManagerApi.Domain.Repositories;
using CourseManagerApi.Domain.ValueObjects;
using CourseManagerApi.Domain.Entities;
using CourseManagerApi.Shared.Commands;
using CourseManagerApi.Shared.Handlers;
using Flunt.Notifications;
using CourseManagerApi.Domain.Commands.ClientCommands;

namespace CourseManagerApi.Domain.Handlers.ClientHandlers;

public class CreateClientHandler : Notifiable<Notification>, IHandler<CreateClientCommand>
{
    private readonly IClientRepository _clientRepository;
    private readonly IOccupationRepository _occupationRepository;
    private readonly ITenantRepository _tenantRepository;

    public CreateClientHandler(IClientRepository clientRepository, IOccupationRepository occupationRepository, ITenantRepository tenantRepository)
    {
        _clientRepository = clientRepository;
        _occupationRepository = occupationRepository;
        _tenantRepository = tenantRepository;
    }

    public async Task<ICommandResult> Handle(CreateClientCommand command, int tenantId)
    {
        command.Validate();
        if (!command.IsValid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possivel cadastrar o cliente", Notifications.Select(x => x.Message));
        }

        var emailExists = await _clientRepository.EmailExistsAsync(command.Email);
        var documentExists = await _clientRepository.DocumentExistsAsync(command.Document);

        if (emailExists) AddNotification("Email", "Este email já está uso");
        if (documentExists) AddNotification("Document", "Este documento já está uso");

        // Create VOs
        var email = new Email(command.Email);
        var name = new Name(command.FirstName, command.LastName, command.BadgeName);
        var document = new Document(command.Document, command.DocumentType);
        var gender = new Gender(command.GenderType, command.GenderDetail);
        var address = new Address(command.Street, command.AddressNumber, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode, command.AddOnAddress);
        var phoneNumber = new PhoneNumber(command.AreaCode, command.PhoneNumber);
        
        // Get entities
        var occupation = await _occupationRepository.FindByIdAsync(command.OccupationId);
        var tenant = await _tenantRepository.FindByIdAsync(tenantId);

        // Create entity
        var client = new Client(email, name, document, gender, address, command.BirthDate, occupation, tenant, command.IsSmoker, command.Observation);

        // Add VO in list
        client.AddPhoneNumber(phoneNumber);

        AddNotifications(email, name, document, gender, address, phoneNumber, client);

        if (!IsValid) return new CommandResult(false, "Não foi possível cadastrar o cliente", Notifications.Select(x => x.Message));

        // Save
        await _clientRepository.CreateAsync(client);

        return new CommandResult(true, "Cliente criado com sucesso");
    }
}