using System.Net;

using Bogus;
using ChatbotPlatform.Api.Tests.IdentityTests.RequestModels;

namespace ChatbotPlatform.Api.Tests.IdentityTests.RegisterTests
{
    public class RegisterTests
    {
        private readonly BaseHttpClient client;
        private readonly Faker faker;

        public RegisterTests()
        {
            this.client = new BaseHttpClient();

            this.faker = new Faker();
        }

        [Theory]
        [MemberData(nameof(RegisterTestData.OKTestData), 
            MemberType = typeof(RegisterTestData))]
        public async Task RegisterAsync_ShouldReturnOk_ForValidUser(
            string password, string confirmPassword)
        {
            // Arrange
            var requestModel = new RegisterRequestModel(
                this.faker.Internet.Email(), password, confirmPassword);
            var content = this.client.CreateJsonContent(requestModel);

            // Act
            var response = await this.client.PostAsync(
                Constants.IndentityApi.Register, content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(RegisterTestData.BadRequestTestData), 
            MemberType = typeof(RegisterTestData))]
        public async Task RegisterAsync_ShouldReturnBadRequest_ForInvalidInput(
            string email, string password, string confirmPassword)
        {
            // Arrange
            var requestModel = new RegisterRequestModel(email, password, confirmPassword);
            var content = this.client.CreateJsonContent(requestModel);

            // Act
            var response = await this.client.PostAsync
                (Constants.IndentityApi.Register, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [MemberData(nameof(RegisterTestData.RegisterConflictTestData), 
            MemberType = typeof(RegisterTestData))]
        public async Task RegisterAsync_ShouldReturnConflict_ForExistingUser(
            RegisterRequestModel requestModel)
        {
            // Arrange
            var content = this.client.CreateJsonContent(requestModel);

            var firstResponse = await this.client.PostAsync(
                Constants.IndentityApi.Register, content);
            firstResponse.EnsureSuccessStatusCode();

            // Act 
            var secondResponse = await this.client.PostAsync(
                Constants.IndentityApi.Register, content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, secondResponse.StatusCode);
        }
    }
}