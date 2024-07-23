namespace ChatbotPlatform.Api.Tests.IdentityTests.ChangePasswordTests
{
    public class ChangePasswordTestData
    {
        public static TheoryData<string, string> BadRequestPasswordTestData =>
            new TheoryData<string, string>
            {
                { "", "newpassword" },// Missing old password
                {"oldpassword", "" },// Missing new password
                { "", "" }, // Missing both passwords
                { "short", "newpassword" }, // Short old password
                { "oldpassword", "short" }, // Short new password
                { "oldpassword", "oldpassword" }, // Password reuse
                { "oldpassword", "password" } // Weak new password
            };

        public static TheoryData<string, string, string> OKPasswordTestData =>
            new TheoryData<string, string, string>
            {
                { "", "", "" },
            };
    }
}


