namespace Olympia.Common
{
    public static class ErrorConstants
    {
        public const string ErrorInputMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match.";
        public const string AgeErrorMessage = "Your age must be between 12 and 65";
        public const string EmailError = "The email is required";
        public const string GenderErrorMessage = "Gender is required. If you wish you can choose 'unknown'";

        public const string CommentLengthMessage = "The content's lenght must be between {0} and {1} characters long.";
    }
}
