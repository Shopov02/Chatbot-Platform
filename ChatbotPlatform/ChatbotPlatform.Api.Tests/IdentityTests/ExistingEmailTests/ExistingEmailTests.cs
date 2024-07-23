using System.Net;
using static ChatbotPlatform.Api.Tests.Constants;

namespace ChatbotPlatform.Api.Tests.IdentityTests.ExistingEmailTests
{
    public class ExistingEmailTests
    {
        private readonly BaseHttpClient client;

        public ExistingEmailTests()
        {
            this.client = new BaseHttpClient();
        }

        [Theory]
        [MemberData(nameof(ExistingEmailTestsData.BadRequestEmailTestData),
            MemberType = typeof(ExistingEmailTestsData))]
        public async Task DoesUserEmailExistAsync_BadRequest_ShouldReturnExpectedStatusCode(
            string email, HttpStatusCode expectedStatusCode)
        {
            // Act
            var url = string.Format(IndentityApi.ExistingEmail, email);
            var response = await this.client.HeadAsync(url);

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(ExistingEmailTestsData.OkRequestEmailTestData),
            MemberType = typeof(ExistingEmailTestsData))]
        public async Task DoesUserEmailExistAsync_OkRequest_ShouldReturnExpectedStatusCode(
            string email, HttpStatusCode expectedStatusCode)
        {
            // Act
            var url = string.Format(IndentityApi.ExistingEmail, email);
            var response = await this.client.HeadAsync(url);

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }
    }
}