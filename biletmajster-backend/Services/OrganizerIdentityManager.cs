using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using biletmajster_backend.Contracts;
using biletmajster_backend.Interfaces;
using biletmajster_backend.Domain;
using biletmajster_backend.Database.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace biletmajster_backend.Services;

public class OrganizerIdentityManager : IOrganizerIdentityManager
{
    private static int _tokenExpirationTimeInSeconds = TimeSpan.FromDays(1).Seconds;

    private readonly IOrganizersRepository _organizersRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<OrganizerIdentityManager> _logger;
    private readonly IMapper _mapper;

    public OrganizerIdentityManager(IOrganizersRepository organizersRepository, IConfiguration configuration,
        ILogger<OrganizerIdentityManager> logger, IMapper mapper)
    {
        _organizersRepository = organizersRepository;
        _configuration = configuration;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Organizer> RegisterOrganizerAsync(string name, string email, string password)
    {
        _logger.LogDebug($"Registering organizer {name} with email {email}");

        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        return await _organizersRepository.CreateOrganizerAsync(name, email, passwordHash, passwordSalt);
    }

    public async Task<Organizer> PatchOrganizerAsync(long organizerId, OrganizerPatchDto newOrganizer)
    {
        _logger.LogDebug($"Patching organizer {newOrganizer.Name}");
        
        CreatePasswordHash(newOrganizer.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var organizerToUpdate = await _organizersRepository.GetOrganizerByIdAsync(organizerId);
        
        organizerToUpdate.Name = newOrganizer.Name;
        organizerToUpdate.PasswordHash = passwordHash;
        organizerToUpdate.PasswordSalt = passwordSalt;

        return await _organizersRepository.UpdateOrganizer(organizerToUpdate);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        _logger.LogDebug($"Trying to login organizer with email {email}");

        var organizer = await _organizersRepository.GetOrganizerByEmailAsync(email);

        if (organizer == null)
        {
            throw new Exception("Organizer not found");
        }

        if (!VerifyPasswordHash(password, organizer.PasswordHash, organizer.PasswordSalt))
        {
            throw new Exception("Invalid password");
        }

        return CreateToken(organizer);
    }

    private string CreateToken(Organizer organizer)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, organizer.Email),
            new Claim(ClaimTypes.Name, organizer.Name)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            _configuration.GetSection("Jwt:Issuer").Value,
            _configuration.GetSection("Jwt:Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddSeconds(_tokenExpirationTimeInSeconds),
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
}