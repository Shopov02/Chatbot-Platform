namespace ChatbotPlatform.Api.Tests
{
    public static class Constants
    {
        public const string BaseAddress = "";
        public const string ApiBaseAddress = BaseAddress + "/api/";

        public static class IndentityApi
        {
            public const string Register = "/users/register";
            public const string Login = "/users/login";
            public const string ChangePassword = "/users/change-password";
            public const string ExistingEmail = "/users/emails/{0}";

            public const string Email = "";
            public const string Password = "";
        }
    }
}
