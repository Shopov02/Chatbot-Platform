namespace ChatbotPlatform.Api.Tests.IdentityTests.RequestModels
{
    public record ChangePasswordRequestModel(string CurrentPassword, string NewPassword);
}
