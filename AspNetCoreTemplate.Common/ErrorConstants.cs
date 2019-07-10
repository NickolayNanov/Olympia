namespace Olympia.Common
{
    public static class ErrorConstants
    {
        public const string ErrorInputMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not matccch.";
        public const string AgeErrorMessage = "Your {0} must be between {2} and {1}";

        public const string CommentLengthMessage = "The content's lenght must be between {0} and {1} characters long.";
    }
}
