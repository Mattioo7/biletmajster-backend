using biletmajster_backend.Interfaces;
using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Repositories.Interfaces;

namespace biletmajster_backend.Services;

public class ConfirmationService : IConfirmationService
{
    private const int Length = 6;
    private static readonly Random Random = new();
    
    private readonly ICustomMailService _mailService;
    private readonly IOrganizersRepository _organizersRepository;
    private readonly IAccountConfirmationCodeRepository _accountConfirmationCodeRepository;
    private readonly ILogger<ConfirmationService> _logger;
    
    public ConfirmationService(ICustomMailService mailService, IOrganizersRepository organizersRepository, IAccountConfirmationCodeRepository accountConfirmationCodeRepository, ILogger<ConfirmationService> logger)
    {
        _mailService = mailService;
        _organizersRepository = organizersRepository;
        _accountConfirmationCodeRepository = accountConfirmationCodeRepository;
        _logger = logger;
    }

    public async Task SendConfirmationRequestAsync(Organizer organizer)
    {
        _logger.LogDebug($"Sending email with confirmation code to {organizer.Email}");
        
        // generate code
        var code = GenerateConfirmationCode();
        
        await _mailService.SendMailAsync(organizer.Name, organizer.Email, "Confirmation code", code);
        
        // set information about confirmation code and status
        await _accountConfirmationCodeRepository.UpdateOrganizerConfirmationCodeAsync(organizer, code);
        await _organizersRepository.UpdateOrganizerAccountStatusAsync(organizer, OrganizerAccountStatus.PendingForConfirmation);
        
        await _organizersRepository.SaveChangesAsync();
    }

    public async Task<string> GetConfirmationCodeAsync(Organizer organizer)
    {
        _logger.LogDebug($"Confirmation codes for organizer {organizer.Email} requested");
        
        var codes =  await _accountConfirmationCodeRepository.GetConfirmationCodesForOrganizerAsync(organizer);
        
        var code = codes.MaxBy(x => x.CreatedAt);

        return code.Code;
    }

    private string GenerateConfirmationCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, Length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}