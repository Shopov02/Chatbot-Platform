using System.Net;

namespace ChatbotPlatform.Api.Tests.IdentityTests.ExistingEmailTests
{
    public class ExistingEmailTestsData
    {
        public static TheoryData<string, HttpStatusCode> BadRequestEmailTestData =>
            new TheoryData<string, HttpStatusCode>
            {
                { "nonexistinguser@example.com", HttpStatusCode.BadRequest }, // Non-existing email
                { "invalid-email-format", HttpStatusCode.BadRequest }, // Invalid format
                { "", HttpStatusCode.BadRequest }, // Empty email
                { "special!email@example.com", HttpStatusCode.BadRequest }, // Special characters
                { "  whitespace@example.com  ", HttpStatusCode.BadRequest }, // Whitespace
                { null, HttpStatusCode.BadRequest }, // Null email
                { "weakpassword", HttpStatusCode.BadRequest }, // Weak new password
                { "john.doe@example..com", HttpStatusCode.BadRequest }, // Double dots in domain
                { "john.doe@example.c", HttpStatusCode.BadRequest }, // Short TLD
                { "john.doe@.example.com", HttpStatusCode.BadRequest }, // Empty local part
                { "john.doe@example.com.", HttpStatusCode.BadRequest }, // Trailing dot in domain
            };

        public static TheoryData<string, HttpStatusCode> OkRequestEmailTestData =>
            new TheoryData<string, HttpStatusCode>
            {
                { "", HttpStatusCode.OK }, // Existing email
                { "", HttpStatusCode.OK }, // Case insensitive
                { "Testemail@example.com", HttpStatusCode.OK } // Case insensitive
            };
    }
}