namespace ChatbotPlatform.Api.Tests.IdentityTests.LoginTests
{
    public class LoginTestsData
    {
        public static TheoryData<string, string> BadRequestLoginData =>
            new TheoryData<string, string>
            {
                { "invalid-email", "ValidPass123!" }, // Invalid email format
                { "valid@example.com", "" }, // Missing password
                { "", "ValidPass123!" }, // Missing email
                { "valid@example.com", "short" }, // Password too short
                { "validexample.com", "ValidPass123!" }, // Email without @
                { "valid@example", "ValidPass123!" }, // Email without domain
                { "another-invalid-email", "AnotherValidPass123!" }, // Another invalid email format
                { "valid2@example.com", "" }, // Another missing password
                { "", "" }, // Empty payload
                { "servererror@example.com", "ValidPass123!" } // Simulated server error
            };

        public static TheoryData<string, string> OKLoginData =>
            new TheoryData<string, string>
            {
                { "", "" },
            };
    }
}
