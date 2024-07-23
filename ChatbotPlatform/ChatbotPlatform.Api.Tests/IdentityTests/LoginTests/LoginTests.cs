using System.Net;

using ChatbotPlatform.Api.Tests.IdentityTests.RequestModels;

namespace ChatbotPlatform.Api.Tests.IdentityTests.LoginTests
{
    public class LoginTests
    {
        private readonly BaseHttpClient client;

        public LoginTests()
        {
            this.client = new BaseHttpClient();
        }

        [Theory]
        [MemberData(nameof(LoginTestsData.OKLoginData), 
            MemberType = typeof(LoginTestsData))]
        public async Task LoginAsync_ShouldReturnOk_ForValidUser(
            string email, string password)
        {
            // Arrange
            await this.client.AuthorizeUser(email, password);

            var requestModel = new LoginRequestModel(email, password);
            var content = this.client.CreateJsonContent(requestModel);

            // Act
            var response = await this.client.PostAsync(
                Constants.IndentityApi.Login,
                content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(LoginTestsData.BadRequestLoginData), 
            MemberType = typeof(LoginTestsData))]
        public async Task LoginAsync_ShouldReturnBadRequest_ForInvalidInput(
            string email, string password)
        {
            // Arrange
            await this.client.AuthorizeUser("", "");

            var requestModel = new LoginRequestModel(email, password);
            var content = this.client.CreateJsonContent(requestModel);

            // Act
            var response = await this.client.PostAsync(
                Constants.IndentityApi.Login, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
