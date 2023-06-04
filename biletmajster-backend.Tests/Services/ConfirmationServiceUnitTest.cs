using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using biletmajster_backend.Domain;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Interfaces;
using biletmajster_backend.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace biletmajster_backend.Tests.Services;

public class ConfirmationServiceUnitTest
{
    private readonly Organizer _organizer = new()
    {
        Id = 1,
        Name = "Test",
        Email = "email@test.com",
        PasswordHash = new byte[] { 1, 2, 3 },
        PasswordSalt = new byte[] { 1, 2, 3 },
        Events = new List<ModelEvent>(),
        Status = OrganizerAccountStatus.Pending
    };

    [Fact]
    public async void ShouldSendConfirmationRequest()
    {
        // Given
        var mailServiceMock = new Mock<ICustomMailService>();
        mailServiceMock.Setup(m =>
                m.SendMailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var accountConfirmationCodeRepositoryMock = new Mock<IAccountConfirmationCodeRepository>();
        accountConfirmationCodeRepositoryMock
            .Setup(a => a.UpdateOrganizerConfirmationCodeAsync(It.Is<Organizer>(o => o.Id == _organizer.Id),
                It.IsAny<string>())).Returns(Task.CompletedTask);

        var organizersRepositoryMock = new Mock<IOrganizersRepository>();
        organizersRepositoryMock
            .Setup(o => o.UpdateOrganizerAccountStatusAsync(It.Is<Organizer>(o => o.Id == _organizer.Id),
                It.Is<OrganizerAccountStatus>(a => a == OrganizerAccountStatus.Pending)))
            .Returns(Task.CompletedTask);

        var loggerMock = new Mock<ILogger<ConfirmationService>>();

        var confirmationService = new ConfirmationService(mailServiceMock.Object, organizersRepositoryMock.Object,
            accountConfirmationCodeRepositoryMock.Object,
            loggerMock.Object);

        // When
        await confirmationService.SendConfirmationRequestAsync(_organizer);

        // Then
        mailServiceMock.Verify(m => m.SendMailAsync(It.IsAny<string>(), It.IsAny<string>(),
            It.Is<string>(s => s == "Confirmation code"), It.IsAny<string>(),
            It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);

        accountConfirmationCodeRepositoryMock.Verify(
            a => a.UpdateOrganizerConfirmationCodeAsync(It.Is<Organizer>(o => o.Id == _organizer.Id),
                It.IsAny<string>()),
            Times.Once);

        organizersRepositoryMock.Verify(
            x => x.UpdateOrganizerAccountStatusAsync(It.Is<Organizer>(o => o.Id == _organizer.Id),
                It.Is<OrganizerAccountStatus>(a => a == OrganizerAccountStatus.Pending)), Times.Once);
    }

    [Fact]
    public async void ShouldGetConfirmationCodeForOrganizer()
    {
        // Given
        var codesList = new List<AccountConfirmationCode>
        {
            new()
            {
                Id = 1,
                Organizer = _organizer,
                Code = "code1",
                CreatedAt = new DateTime(2021, 04, 01)
            },
            new()
            {
                Id = 2,
                Organizer = _organizer,
                Code = "code2",
                CreatedAt = new DateTime(2022, 04, 01)
            }
        };

        var mailServiceMock = new Mock<ICustomMailService>();

        var organizersRepositoryMock = new Mock<IOrganizersRepository>();

        var accountConfirmationCodeRepositoryMock = new Mock<IAccountConfirmationCodeRepository>();
        accountConfirmationCodeRepositoryMock
            .Setup(a => a.GetConfirmationCodesForOrganizerAsync(It.Is<Organizer>(o => o.Id == _organizer.Id)))
            .Returns(Task.FromResult(codesList));

        var loggerMock = new Mock<ILogger<ConfirmationService>>();

        var confirmationService = new ConfirmationService(mailServiceMock.Object, organizersRepositoryMock.Object,
            accountConfirmationCodeRepositoryMock.Object, loggerMock.Object);

        // When
        var result = await confirmationService.GetConfirmationCodeAsync(_organizer);

        // Then
        Assert.Equal("code2", result);
    }
}