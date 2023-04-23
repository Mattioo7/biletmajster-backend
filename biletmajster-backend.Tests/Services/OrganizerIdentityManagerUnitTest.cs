using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using biletmajster_backend.Domain;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace biletmajster_backend.Tests.Services;

public class OrganizerIdentityManagerUnitTest
{
    [Fact]
    public async void ShouldRegisterOrganizerWithCorrectPasswordHash()
    {
        // Given
        var configurationMock = new Mock<IConfiguration>();

        var name = "organizerName";
        var email = "organizerEmail@email.com";
        var password = "secretPassword";

        byte[] passwordHash = null;
        byte[] passwordSalt = null;

        var organizersRepositoryMock = new Mock<IOrganizersRepository>();

        organizersRepositoryMock
            .Setup(o => o.CreateOrganizerAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>(),
                It.IsAny<byte[]>())).Callback<string, string, byte[], byte[]>(
                (_, _, _passwordHash, _passwordSalt) =>
                {
                    passwordHash = _passwordHash;
                    passwordSalt = _passwordSalt;
                });

        var loggerMock = new Mock<ILogger<OrganizerIdentityManager>>();
        var mapperMock = new Mock<IMapper>();

        var organizerIdentityManager =
            new OrganizerIdentityManager(organizersRepositoryMock.Object, configurationMock.Object, loggerMock.Object,
                mapperMock.Object);

        // When
        await organizerIdentityManager.RegisterOrganizerAsync(name, email, password);

        // Then
        organizersRepositoryMock.Verify(
            x => x.CreateOrganizerAsync(It.Is<string>(s => s == name), It.Is<string>(s => s == email),
                It.IsAny<byte[]>(), It.IsAny<byte[]>()), Times.Once);

        Assert.NotNull(passwordHash);
        Assert.NotNull(passwordSalt);

        // verify passwordHash
        using var hmac = new HMACSHA512(passwordSalt!);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        Assert.True(computedHash.SequenceEqual(passwordHash!));
    }

    [Fact]
    public async void ShouldLoginRegisteredOrganizer()
    {
        // Given
        var name = "organizerName";
        var email = "organizerEmail@email.com";
        var password = "secretPassword";

        using var hmac = new HMACSHA512();
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        var organizersRepositoryMock = new Mock<IOrganizersRepository>();
        organizersRepositoryMock
            .Setup(o => o.GetOrganizerByEmailAsync(It.Is<string>(e => e == email)))
            .ReturnsAsync(new Organizer
            {
                Id = 1,
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

        var configurationMock = new Mock<IConfiguration>();

        var tokenSectionMock = new Mock<IConfigurationSection>();
        tokenSectionMock.Setup(t => t.Value)
            .Returns("Yh2k7QSu4l8CZg5p6X3Pna9L0Mic4D3Bvt0Jvr87Uc1j69Kqw5R2Nmf4FWa03Hdx");

        var issuerSectionMock = new Mock<IConfigurationSection>();
        issuerSectionMock.Setup(t => t.Value).Returns("issuer");

        var audienceSectionMock = new Mock<IConfigurationSection>();
        audienceSectionMock.Setup(t => t.Value).Returns("audience");

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Token")))
            .Returns(tokenSectionMock.Object);

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Issuer")))
            .Returns(issuerSectionMock.Object);

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Audience")))
            .Returns(audienceSectionMock.Object);

        var loggerMock = new Mock<ILogger<OrganizerIdentityManager>>();

        var mapperMock = new Mock<IMapper>();

        var organizerIdentityManager =
            new OrganizerIdentityManager(organizersRepositoryMock.Object, configurationMock.Object, loggerMock.Object,
                mapperMock.Object);


        // When
        await organizerIdentityManager.LoginAsync(email, password);
    }

    [Fact]
    public async void ShouldNotLoginUnregisteredOrganizer()
    {
        // Given
        // Given
        var name = "organizerName";
        var email = "organizerEmail@email.com";
        var password = "secretPassword";

        var organizersRepositoryMock = new Mock<IOrganizersRepository>();
        organizersRepositoryMock
            .Setup(o => o.GetOrganizerByEmailAsync(It.Is<string>(e => e == email)))
            .ReturnsAsync((Organizer)null);

        var configurationMock = new Mock<IConfiguration>();

        var tokenSectionMock = new Mock<IConfigurationSection>();
        tokenSectionMock.Setup(t => t.Value)
            .Returns("Yh2k7QSu4l8CZg5p6X3Pna9L0Mic4D3Bvt0Jvr87Uc1j69Kqw5R2Nmf4FWa03Hdx");

        var issuerSectionMock = new Mock<IConfigurationSection>();
        issuerSectionMock.Setup(t => t.Value).Returns("issuer");

        var audienceSectionMock = new Mock<IConfigurationSection>();
        audienceSectionMock.Setup(t => t.Value).Returns("audience");

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Token")))
            .Returns(tokenSectionMock.Object);

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Issuer")))
            .Returns(issuerSectionMock.Object);

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Audience")))
            .Returns(audienceSectionMock.Object);

        var loggerMock = new Mock<ILogger<OrganizerIdentityManager>>();

        var mapperMock = new Mock<IMapper>();

        var organizerIdentityManager =
            new OrganizerIdentityManager(organizersRepositoryMock.Object, configurationMock.Object, loggerMock.Object,
                mapperMock.Object);

        // When
        await Assert.ThrowsAsync<Exception>(async () =>
            await organizerIdentityManager.LoginAsync(email, password));
    }

    [Fact]
    public async void ShouldNotLoginOrganizerWithWrongPassword()
    {
        // Given
        var name = "organizerName";
        var email = "organizerEmail@email.com";
        var password = "secretPassword";

        using var hmac = new HMACSHA512();
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        var organizersRepositoryMock = new Mock<IOrganizersRepository>();
        organizersRepositoryMock
            .Setup(o => o.GetOrganizerByEmailAsync(It.Is<string>(e => e == email)))
            .ReturnsAsync(new Organizer
            {
                Id = 1,
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });

        var configurationMock = new Mock<IConfiguration>();

        var tokenSectionMock = new Mock<IConfigurationSection>();
        tokenSectionMock.Setup(t => t.Value)
            .Returns("Yh2k7QSu4l8CZg5p6X3Pna9L0Mic4D3Bvt0Jvr87Uc1j69Kqw5R2Nmf4FWa03Hdx");

        var issuerSectionMock = new Mock<IConfigurationSection>();
        issuerSectionMock.Setup(t => t.Value).Returns("issuer");

        var audienceSectionMock = new Mock<IConfigurationSection>();
        audienceSectionMock.Setup(t => t.Value).Returns("audience");

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Token")))
            .Returns(tokenSectionMock.Object);

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Issuer")))
            .Returns(issuerSectionMock.Object);

        configurationMock.Setup(c => c.GetSection(It.Is<string>(s => s == "Jwt:Audience")))
            .Returns(audienceSectionMock.Object);

        var loggerMock = new Mock<ILogger<OrganizerIdentityManager>>();

        var mapperMock = new Mock<IMapper>();

        var organizerIdentityManager =
            new OrganizerIdentityManager(organizersRepositoryMock.Object, configurationMock.Object, loggerMock.Object,
                mapperMock.Object);

        // When
        await Assert.ThrowsAsync<Exception>(async () =>
            await organizerIdentityManager.LoginAsync(email, "wrongPassword"));
    }
}