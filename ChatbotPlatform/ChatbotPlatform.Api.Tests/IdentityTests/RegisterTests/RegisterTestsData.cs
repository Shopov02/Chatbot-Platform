using ChatbotPlatform.Api.Tests.IdentityTests.RequestModels;

using Bogus;

namespace ChatbotPlatform.Api.Tests.IdentityTests.RegisterTests
{
    public class RegisterTestData
    {
        private static Faker faker = new Faker();

        public static TheoryData<string, string, string> BadRequestTestData =>
            new TheoryData<string, string, string>
            {
                { "missingpassword@example.com", "", "ValidPass123!" }, // Missing password
                { "invalid-email", "ValidPass123!", "ValidPass123!" }, // Invalid email format
                { "valid@example.com", "weak", "weak" }, // Weak password
                { "valid@example.com", "ValidPass123!", "Mismatch123!" } // Password mismatch
            };

        public static TheoryData<RegisterRequestModel> RegisterConflictTestData =>
            new TheoryData<RegisterRequestModel>
            {
                new RegisterRequestModel(faker.Internet.Email() , "ValidPass1234!", "ValidPass1234!")
            };

        public static TheoryData<string, string> OKTestData =>
            new TheoryData<string, string>
            {
                { "ValidPass123!", "ValidPass123!" },
                { "AnotherValidPass456!", "AnotherValidPass456!" },
                { "ThirdValidPass789!", "ThirdValidPass789!" }
            };
    }
}
