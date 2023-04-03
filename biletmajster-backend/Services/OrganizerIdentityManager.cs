using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Interfaces;
using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Repositories.Interfaces;
using biletmajster_backend.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace biletmajster_backend.Services;

public class OrganizerIdentityManager : IOrganizerIdentityManager
{
    private readonly IOrganizersRepository _organizersRepository;
    private readonly IConfiguration _configuration;
    private readonly Logger<OrganizerIdentityManager> _logger;

    public OrganizerIdentityManager(IOrganizersRepository organizersRepository, IConfiguration configuration,
        Logger<OrganizerIdentityManager> logger)
    {
        _organizersRepository = organizersRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<Organizer> RegisterOrganizerAsync(string name, string email, string password)
    {
        _logger.LogDebug($"Registering organizer {name} with email {email}");
        
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        return await _organizersRepository.CreateOrganizerAsync(name, email, passwordHash, passwordSalt);
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
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}