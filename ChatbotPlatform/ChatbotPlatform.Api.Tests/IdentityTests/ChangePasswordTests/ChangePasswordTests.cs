using System.Net;

using ChatbotPlatform.Api.Tests.IdentityTests.RequestModels;

namespace ChatbotPlatform.Api.Tests.IdentityTests.ChangePasswordTests
{
    public class ChangePasswordTests
    {
        private readonly BaseHttpClient client;

        public ChangePasswordTests()
        {
            this.client = new BaseHttpClient();
        }

        [Theory]
        [MemberData(nameof(ChangePasswordTestData.OKPasswordTestData),
            MemberType = typeof(ChangePasswordTestData))]
        public async Task ChangePasswordAsync_ShouldReturnOk_ForValidInput(
            string email, string oldPassword, string newPassword)
        {
            // Arrange
            await this.client.AuthorizeUser(email, oldPassword);

            var requestModel = new ChangePasswordRequestModel(oldPassword, newPassword);
            var content = this.client.CreateJsonContent(requestModel);

            // Act
            var response = await this.client.PutAsync(
                Constants.IndentityApi.ChangePassword,
                content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(ChangePasswordTestData.BadRequestPasswordTestData), 
            MemberType = typeof(ChangePasswordTestData))]
        public async Task ChangePasswordAsync_ShouldReturnBadRequest_ForInvalidInput(
            string oldPassword, string newPassword)
        {
            // Arrange
            await this.client.AuthorizeUser(
                Constants.IndentityApi.Email, Constants.IndentityApi.Password);

            var requestModel = new ChangePasswordRequestModel(newPassword, oldPassword);
            var content = this.client.CreateJsonContent(requestModel);

            // Act
            var response = await this.client.PutAsync(
                Constants.IndentityApi.ChangePassword,
                content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
